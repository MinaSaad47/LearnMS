import { useLessonsQuery } from "@/api/lessons-api";
import Loading from "@/components/loading/loading";
import { useEffect, useRef } from "react";
import { useParams } from "react-router-dom";

const StudentLessonPage = () => {
  const veRef = useRef<any>();

  const { lessonId, lectureId, courseId } = useParams();

  const { data: lesson, isLoading } = useLessonsQuery({
    lessonId: lessonId!,
    lectureId: lectureId!,
    courseId: courseId!,
  });

  useEffect(() => {
    if (lesson?.data.videoEmbed) {
      veRef.current.innerHTML = lesson?.data.videoEmbed;
    }
  }, [lesson?.data.videoEmbed]);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <div className='flex flex-col items-center justify-center w-full h-full gap-2 p-4'>
      <h2>{lesson?.data.title}</h2>
      <div ref={veRef}></div>
    </div>
  );
};

export default StudentLessonPage;
