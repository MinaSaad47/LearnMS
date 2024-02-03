import { Layout } from "@/components/layout";
import RequireAuth from "@/components/require-auth";
import LoginPage from "@/pages/auth/login-page";
import RegisterPage from "@/pages/auth/register-page";
import CreditCodesPage from "@/pages/credit-codes/credit-code-page";
import FilesPage from "@/pages/files/files-page";
import { Route, Routes } from "react-router-dom";
import Home from "./components/home";
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
      <Route path='/login' element={<LoginPage />} />
      <Route path='/register' element={<RegisterPage />} />

      <Route
        element={
          <RequireAuth>
            <Layout />
          </RequireAuth>
        }>
        <Route path='/' element={<Home />} />

        <Route path='/courses' element={<CoursesPage />} />
        <Route path='/courses/add' element={<AddCoursePage />} />
        <Route path='/courses/:courseId' element={<CourseDetailsPage />} />
        <Route
          path='/courses/:courseId/lectures/:lectureId'
          element={<LectureDetailsPage />}
        />
        <Route
          path='/courses/:courseId/lectures/:lecturesId/lessons/:lessonId'
          element={<LessonPage />}
        />
        <Route
          path='/courses/:courseId/lectures/:lecturesId/quizzes/:quizId'
          element={<QuizPage />}
        />
        <Route path='/courses/:courseId/exams/:examId' element={<ExamPage />} />
        <Route path='/credit-codes' element={<CreditCodesPage />} />
        <Route path='/files' element={<FilesPage />} />
      </Route>
    </Routes>
  );
}

export default App;
