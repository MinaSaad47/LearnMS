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

export const StudentCoursesPage = () => {
  const { data, isLoading, isError: __, error: _ } = useCoursesQuery();

  if (isLoading) {
    return <Loading />;
  }

  console.log(data);

  return (
    <div
      className='absolute z-10 w-full h-full bg-cover'
      style={{
        backgroundImage:
          "url(https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fgetwallpapers.com%2Fwallpaper%2Ffull%2F1%2F6%2Ff%2F891597-best-wallpaper-of-study-2560x1600-notebook.jpg&f=1&nofb=1&ipt=65a05bae173513493b6090faebcb069f825f71a2566ba6a517e34c6c1c80fe84&ipo=images)",
      }}>
      <div className='h-[1200px] w-full py-20 gap-4'>
        <h1 className='text-3xl font-bold text-center text-white mt-72 md:text-5xl'>
          TAKE THE FIRST STEP TO YOUR JOURNEY TO SUCCESS WITH US
        </h1>
      </div>
      <div className='absolute flex flex-wrap w-full gap-4 top-[550px] items-center justify-center'>
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
    <Card className=' bg-white/70 min-h-[300px] border-color2 border-[3px] relative hover:bg-zinc-300 transition-all duration-300 hover:scale-125 hover:cursor-pointer'>
      <Link to={`/courses/${course.id}`}>
        <CardHeader>
          <p>{course.title}</p>
        </CardHeader>
        <CardContent className='object-cover'>
          <img
            className='w-full h-full'
            src={course.imageUrl ?? ""}
            alt='Course Image'
          />
        </CardContent>
        <CardFooter>
          {course.enrollment === "Expired" ? (
            <p>{course.renewalPrice}</p>
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
