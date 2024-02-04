import { useCoursesQuery } from "@/api/courses-api";
import Loading from "@/components/loading/loading";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
} from "@/components/ui/card";
import { Course } from "@/types/courses";
import { Link } from "react-router-dom";

export const StudentCoursesPage = () => {
  const { data, isLoading, isError: __, error: _ } = useCoursesQuery();

  if (isLoading) {
    return <Loading />;
  }

  console.log(data);

  return (
    <div className='flex flex-wrap gap-4 p-10'>
      {data?.data.items.map((course) => (
        <CourseCard key={course.id} course={course} />
      ))}
    </div>
  );
};

function CourseCard({ course }: { course: Course }) {
  return (
    <Card className='relative w-[20%] hover:bg-zinc-300 transition-all duration-300 hover:scale-105 hover:cursor-pointer'>
      <Link to={`/courses/${course.id}`}>
        <CardHeader>
          <p>{course.title}</p>
        </CardHeader>
        <CardContent>
          <img src={course.imageUrl ?? ""} alt='Course Image' />
        </CardContent>
        <CardFooter>
          {course.expiresAt != null && course.isExpired ? (
            <p>{course.renewalPrice}</p>
          ) : (
            <p>{course.price} LE</p>
          )}

          <p className='text-left'>
            {course.expiresAt &&
              !course.isExpired &&
              `Expires on ${new Date(course.expiresAt).toDateString()}`}
          </p>
        </CardFooter>
      </Link>
    </Card>
  );
}
