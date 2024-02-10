import { useCoursesQuery } from "@/api/courses-api";
import Footer from "@/components/footer";
import Loading from "@/components/loading/loading";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
} from "@/components/ui/card";
import { Course } from "@/types/courses";
import { Link } from "react-router-dom";

import CoursesBackground from "@/pages/student/courses/courses-background";

export const StudentCoursesPage = () => {
  const { data, isLoading, isError: __, error: _ } = useCoursesQuery();

  if (isLoading) {
    return <Loading />;
  }

  console.log(data);

  return (
    <div className='z-10 w-full' style={{}}>
      <div className='flex flex-col-reverse items-center justify-around md:flex-row'>
        <h1 className='text-3xl font-bold text-center text-color1 md:text-5xl w-[80%]'>
          TAKE THE FIRST STEP TO YOUR JOURNEY TO SUCCESS WITH US
        </h1>
        <div className='h-[400px] md:h-[600px] w-fit ml-auto'>
          <CoursesBackground />
        </div>
      </div>
      <div className='flex flex-wrap items-center justify-center w-full gap-4 '>
        <div className='z-10 grid w-full grid-cols-1 gap-8 p-20 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4'>
          {data?.data.items.map((course) => (
            <CourseCard key={course.id} course={course} />
          ))}
        </div>
      </div>
      <div className='z-0 bottom-0 rounded-t-[50%]  -translate-y-1/2 bg-white h-full w-full'></div>
      <Footer />
    </div>
  );
};

function CourseCard({ course }: { course: Course }) {
  return (
    <Card className=' bg-white/70 min-h-[300px] shadow-lg rounded-3xl border-0 shadow-color2 relative hover:bg-zinc-300 transition-all duration-300 hover:scale-110 hover:cursor-pointer'>
      <Link
        className='flex flex-col justify-between h-full'
        to={`/courses/${course.id}`}>
        <CardHeader className='text-white rounded-t-3xl bg-color2'>
          <p>{course.title}</p>
        </CardHeader>
        <CardContent className='object-cover'>
          <img
            className='w-full h-full'
            src={course.imageUrl ?? ""}
            alt='Course Image'
          />
        </CardContent>
        <CardFooter className='flex flex-col items-end justify-center p-4 text-xl font-bold text-center text-white rounded-b-3xl bg-color1'>
          {course.enrollment === "Expired" ? (
            <p>{course.renewalPrice} LE</p>
          ) : (
            <p>{course.price} LE</p>
          )}

          <p className='text-left'>
            {course.enrollment === "Expired" &&
              `Expires on ${new Date(course.expiresAt!).toDateString()}`}
          </p>
        </CardFooter>
      </Link>
    </Card>
  );
}
