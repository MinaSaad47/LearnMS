import { useBuyCourseMutation, useCourseQuery } from "@/api/courses-api";
import Loading from "@/components/loading/loading";
import {
  AlertDialog,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
import { cn } from "@/lib/utils";
import { CourseDetails } from "@/types/courses";
import { Exam } from "@/types/exams";
import { Lecture } from "@/types/lectures";
import { useState } from "react";
import { FaSpinner } from "react-icons/fa";
import { Link, useParams } from "react-router-dom";

export const StudentCoursePage = () => {
  const { courseId } = useParams();

  const { isLoading, data } = useCourseQuery(courseId!);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <div>
      <CourseHeader course={data?.data!} />
      <div className='grid items-center justify-center grid-cols-1 gap-4 p-10 lg:grid-cols-4 md:grid-cols-3'>
        {data?.data.items.map((item) => (
          <CourseItem key={item.order} item={item} courseId={courseId!} />
        ))}
      </div>
    </div>
  );
};

function CourseHeader({ course }: { course: CourseDetails }) {
  return (
    <Card className='m-4 text-white bg-color2 rounded-2xl'>
      <div className='flex justify-between w-full h-full gap-2 overflow-clip'>
        <div className='flex flex-col w-full'>
          <CardHeader>
            <h2 className='text-2xl italic font-bold text-start'>
              {course.title}
            </h2>
          </CardHeader>
          <CardContent>
            <p>{course.description}</p>
          </CardContent>
          {course.enrollment !== "Active" ? (
            <BuyButton course={course} />
          ) : (
            <Badge variant='destructive' className='self-end w-fit'>
              Expires on {new Date(course.expiresAt!).toDateString()}
            </Badge>
          )}
        </div>
        <div className='h-full overflow-clip w-[300px] rounded aspect-square'>
          <img
            src={course.imageUrl ?? ""}
            alt=''
            className='object-cover w-full h-full'
          />
        </div>
      </div>
    </Card>
  );
}

function CourseItem({
  item,
  courseId,
}: {
  item: Lecture | Exam;
  courseId: string;
}) {
  return (
    <Card className='relative transition-all duration-300 shadow-lg rounded-2xl shadow-color2 hover:bg-color2/20 hover:scale-105 hover:cursor-pointer'>
      <Link to={`/courses/${courseId}/${item.type.toLowerCase()}s/${item.id}`}>
        <CardHeader className='text-white rounded-t-2xl bg-color2'>
          {item.title}
        </CardHeader>
        {item.type == "Lecture" && (
          <CardContent>
            <img src={item.imageUrl ?? ""} alt={`${item.type} Image`} />
          </CardContent>
        )}
        <CardHeader>
          {item.type === "Lecture" && item.enrollment === "Expired" ? (
            <p>{item.renewalPrice}</p>
          ) : (
            <p>{item.price} LE</p>
          )}

          <p className='text-left'>
            {item.type === "Lecture" &&
              item.enrollment === "Active" &&
              `Expires on ${new Date(item.expiresAt).toDateString()}`}
          </p>
          <div className='flex justify-end'>
            <Badge
              className={cn(item.type === "Lecture" && "bg-color2")}
              variant={item.type == "Exam" ? "destructive" : "default"}>
              {item.type}
            </Badge>
          </div>
        </CardHeader>
      </Link>
    </Card>
  );
}

export function BuyButton({ course }: { course: CourseDetails }) {
  const [isOpen, setIsOpen] = useState(false);
  const { mutate, isPending } = useBuyCourseMutation();

  const onClick = () => {
    mutate(
      { courseId: course.id },
      {
        onSuccess: () => {
          setIsOpen(false);
        },
      }
    );
  };

  return (
    <AlertDialog open={isOpen} onOpenChange={setIsOpen}>
      <AlertDialogTrigger
        onClick={() => setIsOpen(!isOpen)}
        className='mt-auto mb-2 ml-auto mr-2 transition-all bg-white shadow-md duration-400 text-color1 hover:bg-color1 hover:text-white w-fit hover:scale-115'
        asChild>
        <Button variant='outline'>
          {course.enrollment === "NotEnrolled" && (
            <div>Buy for {course.price} LE</div>
          )}
          {course.enrollment === "Expired" && (
            <div>Renew for {course.renewalPrice} LE</div>
          )}
        </Button>
      </AlertDialogTrigger>
      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone
          </AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
          <AlertDialogCancel>Cancel</AlertDialogCancel>
          <Button onClick={onClick} disabled={isPending}>
            {isPending ? <FaSpinner className='animate-spin' /> : "Confirm"}
          </Button>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}
