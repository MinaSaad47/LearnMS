import { DashboardLayout } from "@/components/dashboard-layout";
import RequireAuth from "@/components/require-auth";
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

      <Route path='/' element={<StudentLayout />}>
        <Route path='/' element={<StudentHomePage2 />} />
      </Route>

      <Route
        path='/'
        element={
          <RequireAuth role='Student'>
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
          <RequireAuth role='Teacher'>
            <DashboardLayout />
          </RequireAuth>
        }>
        <Route path='courses' element={<CoursesPage />} />
        <Route path='courses/add' element={<AddCoursePage />} />
        <Route path='courses/:courseId' element={<CourseDetailsPage />} />
        <Route
          path='courses/:courseId/lectures/:lectureId'
          element={<LectureDetailsPage />}
        />
        <Route
          path='courses/:courseId/lectures/:lectureId/lessons/:lessonId'
          element={<LessonDetailsPage />}
        />
        <Route
          path='courses/:courseId/lectures/:lecturesId/quizzes/:quizId'
          element={<QuizPage />}
        />
        <Route path='courses/:courseId/exams/:examId' element={<ExamPage />} />
        <Route path='credit-codes' element={<CreditCodesPage />} />
        <Route path='files' element={<FilesPage />} />
        <Route path='assistants' element={<AssistantsPage />} />
        <Route path='students' element={<StudentsPage />} />
      </Route>
    </Routes>
  );
}

export default App;
