import { useCourseQuery } from "@/api/courses-api";
import { useLectureQuery } from "@/api/lectures-api";
import Loading from "@/components/loading/loading";
import { Badge } from "@/components/ui/badge";
import { Card, CardFooter, CardTitle } from "@/components/ui/card";
import { toast } from "@/components/ui/use-toast";
import { Lesson } from "@/types/lessons";
import { Quiz } from "@/types/quiz";
import { useNavigate, useParams } from "react-router-dom";

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

  console.log(course);
  console.log(lecture);

  return (
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
  );
};

export default StudentLecturePage;

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
      className='relative m-auto w-[80%] p-3 transition-all duration-300 hover:bg-blue-300 hover:scale-105 hover:cursor-pointer bg-blue-400 text-white'>
      <CardTitle>{item.title}</CardTitle>
      <CardFooter>
        <div className='flex justify-end w-full gap-2'>
          <Badge variant={item.type == "Quiz" ? "destructive" : "default"}>
            {item.type}
          </Badge>
          {isLocked && <Badge variant='secondary'>Locked</Badge>}
        </div>
      </CardFooter>
    </Card>
  );
}
