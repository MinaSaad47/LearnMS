import { useCoursesQuery } from "@/api/courses-api";
import { DataTable } from "@/components/data-table";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import { PlusCircle } from "lucide-react";
import { Link } from "react-router-dom";
import { coursesColumns } from "./columns";

const CoursesPage = () => {
  const { data: courses, isLoading } = useCoursesQuery();

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />;
      </div>
    );
  }

  return (
    <div className='flex flex-col w-full'>
      <Link className='self-end m-2' to={"/dashboard/courses/add"}>
        <Button>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Course
        </Button>
      </Link>
      <div className='m-4'>
        <DataTable columns={coursesColumns} data={courses?.data!.items!} />
      </div>
    </div>
  );
};

export default CoursesPage;
