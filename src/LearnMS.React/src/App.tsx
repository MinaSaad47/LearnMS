import { DashboardLayout } from "@/components/dashboard-layout";
import RequireAuth from "@/components/require-auth";
import PasswordResetPage from "@/pages/auth/password-reset-page";
import SignInSignUpPage from "@/pages/auth/sign-in-sign-up-page";
import AssistantsPage from "@/pages/dashboard/assistants/assistants-page";
import AddCoursePage from "@/pages/dashboard/courses/add-course-page";
import CourseDetailsPage from "@/pages/dashboard/courses/course-details-page";
import CoursesPage from "@/pages/dashboard/courses/courses-page";
import CreditCodesPage from "@/pages/dashboard/credit-codes/credit-code-page";
import ExamPage from "@/pages/dashboard/exams/exam-page";
import FilesPage from "@/pages/dashboard/files/files-page";
import LectureDetailsPage from "@/pages/dashboard/lectures/lecture-details-page";
import LessonDetailsPage from "@/pages/dashboard/lessons/lesson-details-page";
import QuizPage from "@/pages/dashboard/quizzes/quiz-page";
import StudentDetailsPage from "@/pages/dashboard/students/student-details-page";
import StudentsPage from "@/pages/dashboard/students/students-page";
import { StudentCoursePage } from "@/pages/student/courses/student-course-page";
import { StudentCoursesPage } from "@/pages/student/courses/student-courses-page";
import StudentHomePage2 from "@/pages/student/home/student-home.page";
import StudentLecturePage from "@/pages/student/lectures/student-lecture-page";
import StudentLessonPage from "@/pages/student/lessons/student-lesson-page";
import StudentPayments from "@/pages/student/payment/student-payments";
import { Route, Routes } from "react-router-dom";
import StudentLayout from "./components/student-layout";

function App() {
  return (
    <Routes>
      <Route path='/sign-in-sign-up' element={<SignInSignUpPage />} />
      <Route path='/auth/reset-password' element={<PasswordResetPage />} />

      <Route path='/' element={<StudentLayout />}>
        <Route path='/' element={<StudentHomePage2 />} />
      </Route>

      <Route
        path='/'
        element={
          <RequireAuth roles={["Student"]}>
            <StudentLayout />
          </RequireAuth>
        }>
        <Route path='courses' element={<StudentCoursesPage />} />
        <Route path='courses/:courseId' element={<StudentCoursePage />} />
        <Route
          path='courses/:courseId/lectures/:lectureId'
          element={<StudentLecturePage />}
        />
        <Route
          path='courses/:courseId/lectures/:lectureId/lessons/:lessonId'
          element={<StudentLessonPage />}
        />
        <Route path='payments' element={<StudentPayments />} />
      </Route>

      <Route
        path='/dashboard'
        element={
          <RequireAuth roles={["Teacher", "Assistant"]}>
            <DashboardLayout />
          </RequireAuth>
        }>
        <Route
          path='courses'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <CoursesPage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/add'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <AddCoursePage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/:courseId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <CourseDetailsPage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/:courseId/lectures/:lectureId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <LectureDetailsPage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/:courseId/lectures/:lectureId/lessons/:lessonId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <LessonDetailsPage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/:courseId/lectures/:lecturesId/quizzes/:quizId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <QuizPage />
            </RequireAuth>
          }
        />
        <Route
          path='courses/:courseId/exams/:examId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageCourses"]}>
              <ExamPage />
            </RequireAuth>
          }
        />
        <Route path='credit-codes' element={<CreditCodesPage />} />
        <Route
          path='files'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageFiles"]}>
              <FilesPage />
            </RequireAuth>
          }
        />
        <Route
          path='assistants'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageAssistants"]}>
              <AssistantsPage />
            </RequireAuth>
          }
        />
        <Route
          path='students'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageStudents"]}>
              <StudentsPage />
            </RequireAuth>
          }
        />
        <Route
          path='students/:studentId'
          element={
            <RequireAuth
              roles={["Teacher", "Assistant"]}
              permissions={["ManageStudents"]}>
              <StudentDetailsPage />
            </RequireAuth>
          }
        />
      </Route>
    </Routes>
  );
}

export default App;
