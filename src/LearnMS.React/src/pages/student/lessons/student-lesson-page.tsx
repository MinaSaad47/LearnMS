import { useLessonsQuery } from "@/api/lessons-api";
import Loading from "@/components/loading/loading";
import { useRef } from "react";
import { useParams } from "react-router-dom";

const StudentLessonPage = () => {
  const veRef = useRef<any>();

  const { lessonId, lectureId, courseId } = useParams();

  const { data: lesson, isLoading } = useLessonsQuery({
    lessonId: lessonId!,
    lectureId: lectureId!,
    courseId: courseId!,
  });

  if (isLoading) {
    return (
      <div className='w-full h-full'>
        <Loading />
      </div>
    );
  }

  return (
    <div className='flex flex-col items-center w-full h-full gap-10 p-4'>
      <h1 className='text-3xl'>{lesson?.data.title}</h1>
      <div className='w-[80%] rounded-xl aspect-video overflow-clip'>
        <iframe
          src={lesson?.data.videoSrc!}
          allowFullScreen
          className='object-cover w-full h-full'
          allow='encrypted-media'></iframe>
      </div>
      <div className='w-[80%] p-4 text-blue-600 bg-blue-100 '>
        <p>{lesson?.data.description}</p>
      </div>
    </div>
  );
};

export default StudentLessonPage;
