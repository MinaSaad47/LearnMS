import {
  useLessonsQuery,
  useRenewLessonMutation,
  useStartLessonMutation,
} from "@/api/lessons-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { useParams } from "react-router-dom";

const StudentLessonPage = () => {
  const { lessonId, lectureId, courseId } = useParams();

  const { data: lesson, isLoading } = useLessonsQuery({
    lessonId: lessonId!,
    lectureId: lectureId!,
    courseId: courseId!,
  });

  const startLessonMutation = useStartLessonMutation();
  const renewLessonMutation = useRenewLessonMutation();

  if (isLoading) {
    return (
      <div className='w-full h-full'>
        <Loading />
      </div>
    );
  }

  const onStarting = () => {
    startLessonMutation.mutate({
      courseId: courseId!,
      lectureId: lectureId!,
      lessonId: lessonId!,
    });
  };

  const onRenewing = () => {
    renewLessonMutation.mutate({
      courseId: courseId!,
      lectureId: lectureId!,
      lessonId: lessonId!,
    });
  };

  if (lesson?.data.enrollment === "NotEnrolled") {
    return (
      <div className='flex flex-col items-center justify-center w-full h-full gap-6'>
        <h1 className='text-3xl'>Start the lesson</h1>
        <p className='text-center'>
          {`Are you sure you want to start the lesson? you will have ${lesson.data.expirationHours} hours to complete it, after which it will be locked for ${lesson.data.renewalPrice} LE`}
        </p>

        <Confirmation
          title='Starting the lesson Confirmation'
          description='Are you sure you want to start the lesson?'
          onConfirm={onStarting}
          button={<Button>Start</Button>}
        />
      </div>
    );
  }

  if (lesson?.data.enrollment === "Expired") {
    return (
      <div className='flex flex-col items-center justify-center w-full h-full gap-6'>
        <h1 className='text-3xl'>Lesson expired</h1>
        <p className='text-center'>
          {`The lesson has expired. you can renew it for ${lesson.data.renewalPrice} LE`}
        </p>

        <Confirmation
          title='Renewing the lesson Confirmation'
          description={`Are you sure you want to renew the lesson for ${lesson.data.renewalPrice}?`}
          onConfirm={onRenewing}
          button={<Button>Renew</Button>}
        />
      </div>
    );
  }

  return (
    <div className='flex flex-col items-center w-full h-full gap-10 p-4'>
      <div className='flex flex-col items-center justify-between w-[80%] gap-4'>
        <h1 className='self-start md:text-3xl'>{lesson?.data.title}</h1>
        <Badge className='self-end'>
          expires at {new Date(lesson!.data.expiresAt).toLocaleString()}
        </Badge>
      </div>
      <div className='w-full md:w-[80%] rounded-xl aspect-video overflow-clip'>
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
