import { useCourseQuery } from "@/api/courses-api";
import Loading from "@/components/loading/loading";
import { Badge } from "@/components/ui/badge";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
import { Exam } from "@/types/exams";
import { Lecture } from "@/types/lectures";
import { Link, useParams } from "react-router-dom";

export const StudentCoursePage = () => {
  const { courseId } = useParams();

  const { isLoading, data } = useCourseQuery(courseId!);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <div className='flex flex-wrap gap-4 p-10'>
      {data?.data.items.map((item) => (
        <CourseItem key={item.order} item={item} courseId={courseId!} />
      ))}
    </div>
  );
};

function CourseItem({
  item,
  courseId,
}: {
  item: Lecture | Exam;
  courseId: string;
}) {
  return (
    <Card className='relative w-[20%] hover:bg-zinc-300 transition-all duration-300 hover:scale-105 hover:cursor-pointer'>
      <Link to={`/courses/${courseId}/${item.type.toLowerCase()}s/${item.id}`}>
        <CardHeader>{item.title}</CardHeader>
        {item.type == "Lecture" && (
          <CardContent>
            <img src={item.imageUrl ?? ""} alt={`${item.type} Image`} />
          </CardContent>
        )}
        <CardHeader>
          {item.expiresAt != null && item.isExpired ? (
            <p>{item.renewalPrice}</p>
          ) : (
            <p>{item.price} LE</p>
          )}

          <p className='text-left'>
            {item.expiresAt &&
              !item.isExpired &&
              `Expires on ${item.expiresAt.toDateString()}`}
          </p>
          <div className='flex justify-end'>
            <Badge variant={item.type == "Exam" ? "destructive" : "default"}>
              {item.type}
            </Badge>
          </div>
        </CardHeader>
      </Link>
    </Card>
  );
}
