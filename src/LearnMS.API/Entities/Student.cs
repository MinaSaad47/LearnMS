using LearnMS.API.Common;
using LearnMS.API.Features.Courses;
using LearnMS.API.Features.Profile;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnMS.API.Entities;

[JsonConverter(typeof(StringEnumConverter))]
public enum StudentLevel
{
    Level0,
    Level1,
    Level2,
    Level3
}

public class Student : User
{

    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ParentPhoneNumber { get; set; }
    public required string SchoolName { get; set; }
    public required StudentLevel Level { get; set; }

    private decimal _credit = 0; // DON'T CHANGE THE NAME

    public decimal Credit { get => _credit; }
    public void AddCredit(Guid? assistantId, decimal amount, out StudentCredit studentCredit)
    {
        _credit += amount;
        studentCredit = new StudentCredit
        {
            AssistantId = assistantId,
            StudentId = Id,
            Value = amount
        };
    }

    public void RedeemCode(CreditCode code, out CreditCode redeemedCode)
    {
        _credit += code.Value;
        redeemedCode = code;
        redeemedCode.StudentId = Id;
    }

    public void BuyCourse(Course course, out StudentCourse boughtStudentCourse)
    {
        if (_credit < course.Price) throw new ApiException(ProfileErrors.InsufficientCredits);
        _credit -= course.Price ?? 0;
        boughtStudentCourse = new StudentCourse
        {
            CourseId = course.Id,
            ExpirationDate = DateTime.UtcNow.AddDays(course.ExpirationDays ?? 0),
            StudentId = Id
        };
    }

    public void RenewCourse(Course course, StudentCourse studentCourse, out StudentCourse renewedStudentCourse)
    {
        if (studentCourse.ExpirationDate > DateTime.UtcNow) throw new ApiException(CoursesErrors.AlreadyPurchased);
        if (_credit < course.RenewalPrice) throw new ApiException(ProfileErrors.InsufficientCredits);
        _credit -= course.RenewalPrice ?? 0;
        renewedStudentCourse = studentCourse;
        renewedStudentCourse.ExpirationDate = DateTime.UtcNow.AddDays(course.ExpirationDays ?? 0);
    }

    public void BuyLecture(Lecture lecture, StudentCourse? studentCourse, out StudentLecture boughtStudentLecture)
    {
        if (studentCourse?.ExpirationDate > DateTime.UtcNow) throw new ApiException(CoursesErrors.AlreadyPurchased);
        if (_credit < lecture.Price) throw new ApiException(ProfileErrors.InsufficientCredits);
        _credit -= lecture.Price ?? 0;
        boughtStudentLecture = new StudentLecture
        {
            LectureId = lecture.Id,
            ExpirationDate = DateTime.UtcNow.AddDays(lecture.ExpirationDays ?? 0),
            StudentId = Id
        };
    }

    public void RenewLecture(Lecture lecture, StudentCourse? studentCourse, StudentLecture studentLecture, out StudentLecture renewedStudentLecture)
    {
        if (studentCourse?.ExpirationDate > DateTime.UtcNow) throw new ApiException(CoursesErrors.AlreadyPurchased);
        if (studentLecture.ExpirationDate > DateTime.UtcNow) throw new ApiException(LecturesErrors.AlreadyPurchased);
        if (_credit < lecture.RenewalPrice) throw new ApiException(ProfileErrors.InsufficientCredits);
        _credit -= lecture.RenewalPrice ?? 0;
        renewedStudentLecture = studentLecture;
        renewedStudentLecture.ExpirationDate = DateTime.UtcNow.AddDays(lecture.ExpirationDays ?? 0);
    }

    public void RenewLesson(Lesson lesson, StudentLesson studentLesson)
    {
        if (studentLesson.ExpirationDate > DateTime.UtcNow) throw new ApiException(LessonsErrors.AlreadyRenewed);
        if (_credit < lesson.RenewalPrice) throw new ApiException(ProfileErrors.InsufficientCredits);
        _credit -= lesson.RenewalPrice;
    }


    public static Student Register(Account account, string fullName, string phoneNumber, string parentPhoneNumber, string schoolName, StudentLevel level)
    {
        var id = Guid.NewGuid();

        account.Id = id;

        return new Student
        {
            Id = id,
            FullName = fullName,
            PhoneNumber = phoneNumber,
            ParentPhoneNumber = parentPhoneNumber,
            Level = level,
            SchoolName = schoolName,
            Accounts = new List<Account>() {
               account
            }
        };
    }

    private Student() { }
}