import { DashboardLayout } from "@/components/dashboard-layout";
import RequireAuth from "@/components/require-auth";
import LoginPage from "@/pages/auth/login-page";
import RegisterPage from "@/pages/auth/register-page";
import SignInSignUpPage from "@/pages/auth/sign-in-sign-up-page";
import CreditCodesPage from "@/pages/credit-codes/credit-code-page";
import FilesPage from "@/pages/files/files-page";
import { StudentCoursePage } from "@/pages/student/courses/student-course-page";
import { StudentCoursesPage } from "@/pages/student/courses/student-courses-page";
import StudentLecturePage from "@/pages/student/lectures/student-lecture-page";
import StudentLessonPage from "@/pages/student/lessons/student-lesson-page";
import StudentPayments from "@/pages/student/payment/student-payments";
import { Route, Routes } from "react-router-dom";
import StudentLayout from "./components/student-layout";
import AddCoursePage from "./pages/courses/add-course-page";
import CourseDetailsPage from "./pages/courses/course-details-page";
import CoursesPage from "./pages/courses/courses-page";
import ExamPage from "./pages/exams/exam-page";
import LectureDetailsPage from "./pages/lectures/lecture-details-page";
import LessonPage from "./pages/lessons/lesson-page";
import QuizPage from "./pages/quizzes/quiz-page";

function App() {
  return (
    <Routes>
      <Route path='/sign-in-sign-up' element={<SignInSignUpPage />} />
      <Route path='/login' element={<LoginPage />} />
      <Route path='/register' element={<RegisterPage />} />

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
          path='courses/:courseId/lectures/:lecturesId/lessons/:lessonId'
          element={<LessonPage />}
        />
        <Route
          path='courses/:courseId/lectures/:lecturesId/quizzes/:quizId'
          element={<QuizPage />}
        />
        <Route path='courses/:courseId/exams/:examId' element={<ExamPage />} />
        <Route path='credit-codes' element={<CreditCodesPage />} />
        <Route path='files' element={<FilesPage />} />
      </Route>
    </Routes>
  );
}

export default App;
