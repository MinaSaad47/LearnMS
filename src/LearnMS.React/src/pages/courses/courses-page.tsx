import { Button } from "@/components/ui/button";
import { Course } from "@/types/courses";
import { PlusCircle } from "lucide-react";
import { Link } from "react-router-dom";
import CoursesTable from "./courses-table";

const courses: Course[] = Array.from({ length: 20 }).map((_, index) => ({
  id: index.toString(),
  name: "Course Name",
  description: "Course Description",
  coverUrl: "https://picsum.photos/200/300",
  price: 9.99,
  renewPrice: 9.99,
}));

const CoursesPage = () => {
  return (
    <div className='flex flex-col w-full'>
      <Link className='self-end m-2' to={"/dashboard/courses/add"}>
        <Button>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Course
        </Button>
      </Link>
      <div className='m-4'>
        <CoursesTable courses={courses} />
      </div>
    </div>
  );
};

export default CoursesPage;
