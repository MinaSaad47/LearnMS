import { useCourseQuery } from "@/api/courses-api";
import { useBuyLectureMutation, useLectureQuery } from "@/api/lectures-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { toast } from "@/components/ui/use-toast";
import { cn } from "@/lib/utils";
import { CourseDetails } from "@/types/courses";
import { Lesson } from "@/types/lessons";
import { Quiz } from "@/types/quiz";
import { useNavigate, useParams } from "react-router-dom";
import { LectureDetails } from "../../../types/lectures";

const StudentLecturePage = () => {
  const { lectureId, courseId } = useParams();

  const { isLoading: isLectureLoading, data: lecture } = useLectureQuery({
    lectureId: lectureId!,
    courseId: courseId!,
  });

  const { isLoading: isCourseLoading, data: course } = useCourseQuery(
    courseId!
  );

  if (isLectureLoading || isCourseLoading) {
    return <Loading />;
  }

  return (
    <div>
      <LectureHeader lecture={lecture?.data!} course={course?.data!} />
      <div className='flex flex-col items-center justify-center gap-2 py-4'>
        {lecture?.data.items.map((item) => (
          <LectureItem
            key={item.id}
            isLocked={
              course?.data.enrollment !== "Active" &&
              lecture?.data.enrollment !== "Active"
            }
            item={item}
            courseId={courseId!}
            lectureId={lectureId!}
          />
        ))}
      </div>
    </div>
  );
};

export default StudentLecturePage;

function LectureHeader({
  lecture,
  course,
}: {
  lecture: LectureDetails;
  course: CourseDetails;
}) {
  const buyLectureMutation = useBuyLectureMutation();

  const onBuying = () => {
    buyLectureMutation.mutate(
      {
        lectureId: lecture.id,
        courseId: course.id,
      },
      {
        onSuccess: () => {
          toast({ title: "Purchase Successful" });
        },
      }
    );
  };

  return (
    <Card className='m-4 text-white bg-color2 rounded-2xl'>
      <div className='flex justify-between w-full h-full gap-2 overflow-clip'>
        <div className='flex flex-col w-full'>
          <CardHeader>
            <h2 className='text-2xl italic font-bold text-start'>
              {lecture.title}
            </h2>
          </CardHeader>
          <CardContent>
            <p>{lecture.description}</p>
          </CardContent>

          {lecture.enrollment !== "Active" &&
            course.enrollment !== "Active" && (
              <Confirmation
                button={
                  <Button className='mt-auto mb-2 ml-auto mr-2 transition-all bg-white shadow-md duration-400 text-color1 hover:bg-color1 hover:text-white w-fit hover:scale-115'>
                    {lecture.enrollment === "Expired"
                      ? `Renew for ${lecture.renewalPrice} LE`
                      : `Buy for ${lecture.price} LE`}
                  </Button>
                }
                description='Are you sure you want to buy this lecture?'
                onConfirm={onBuying}
                title='Confirm Purchase'
                disabled={buyLectureMutation.isPending}
              />
            )}
          <div className='flex flex-col items-center gap-2'>
            {lecture.enrollment === "Active" && (
              <Badge variant='destructive' className='self-end w-fit'>
                Lecture expires on {new Date(lecture.expiresAt!).toDateString()}
              </Badge>
            )}
            {course.enrollment === "Active" && (
              <Badge variant='destructive' className='self-end w-fit'>
                Course expires on {new Date(course.expiresAt!).toDateString()}
              </Badge>
            )}
          </div>
        </div>
        <div className='h-full overflow-clip w-[300px] rounded aspect-square'>
          <img
            src={lecture.imageUrl ?? ""}
            alt=''
            className='object-cover w-full h-full'
          />
        </div>
      </div>
    </Card>
  );
}

function LectureItem({
  item,
  courseId,
  lectureId,
  isLocked,
}: {
  item: Lesson | Quiz;
  courseId: string;
  lectureId: string;
  isLocked: boolean;
}) {
  const navigate = useNavigate();

  const onClick = () => {
    if (!isLocked) {
      navigate(`/courses/${courseId}/lectures/${lectureId}/lessons/${item.id}`);
      return;
    }

    toast({
      title: "Lesson is locked",
      description: "Please make a purchase the lecture or the course first",
      variant: "destructive",
    });
  };

  return (
    <Card
      onClick={onClick}
      className='relative border-none m-auto w-[80%] p-3 transition-all duration-300 shadow-lg hover:shadow-color2 rounded-2xl hover:scale-105 hover:cursor-pointer  bg-color2/75 hover:bg-color2 text-white'>
      <CardTitle>{item.title}</CardTitle>
      <CardFooter>
        <div className='flex justify-end w-full gap-2'>
          <Badge
            className={cn(
              item.type !== "Quiz" &&
                "bg-color1/75 hover:bg-white hover:text-color1"
            )}
            variant={item.type == "Quiz" ? "destructive" : "default"}>
            {item.type}
          </Badge>
          {isLocked && <Badge variant='secondary'>Locked</Badge>}
        </div>
      </CardFooter>
    </Card>
  );
}
