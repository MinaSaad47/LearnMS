import {
  UpdateLessonRequest,
  useDeleteLessonMutation,
  useLessonsQuery,
  useUpdateLessonMutation,
} from "@/api/lessons-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { toast } from "@/components/ui/use-toast";
import { LessonDetails } from "@/types/lessons";
import { zodResolver } from "@hookform/resolvers/zod";
import { ListCollapse, Settings2 } from "lucide-react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";

const LessonDetailsPage = () => {
  const { courseId, lectureId, lessonId } = useParams();
  const navigate = useNavigate();

  const {
    data: lesson,
    isLoading,
    isError,
  } = useLessonsQuery({
    courseId: courseId!,
    lectureId: lectureId!,
    lessonId: lessonId!,
  });

  const deleteLessonMutation = useDeleteLessonMutation();

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  const onDeleting = () => {
    deleteLessonMutation.mutate(
      {
        lectureId: lectureId!,
        courseId: courseId!,
        lessonId: lessonId!,
      },
      {
        onSuccess() {
          navigate(`/dashboard/courses/${courseId}/lectures/${lectureId}`, {
            replace: true,
          });
          toast({
            title: "Deleting",
            description: "Successfully deleted the lesson",
          });
        },
      }
    );
  };

  if (isError) {
    return;
  }

  return (
    <div className='w-full h-full p-4'>
      <div className='flex justify-between w-full'>
        <h1 className='text-3xl'>Lesson Setup</h1>
        <div className='flex gap-2 item-center'>
          <Confirmation
            button={<Button variant='destructive'>Delete</Button>}
            title='Are you sure you want to delete this lesson?'
            description='This action cannot be undone.'
            onConfirm={onDeleting}
          />
        </div>
      </div>

      <div className='grid w-full grid-cols-2 mt-10'>
        <LessonDetailsContent
          {...lesson?.data!}
          courseId={courseId!}
          lectureId={lectureId!}
        />
        <LessonVideo videoSrc={lesson?.data.videoSrc!} />
      </div>
    </div>
  );
};

function LessonDetailsContent({
  id,
  description,
  title,
  expirationHours,
  renewalPrice,
  videoSrc,
  courseId,
  lectureId,
}: LessonDetails & { lectureId: string; courseId: string }) {
  const updateLessonMutation = useUpdateLessonMutation();

  const form = useForm<UpdateLessonRequest>({
    resolver: zodResolver(UpdateLessonRequest),
    defaultValues: {
      description,
      title,
      videoSrc,
      expirationHours,
      renewalPrice,
    },
    values: { description, title, videoSrc, expirationHours, renewalPrice },
  });

  const onSubmit = (data: UpdateLessonRequest) => {
    updateLessonMutation.mutate(
      { lectureId, lessonId: id, courseId, data },
      {
        onSuccess: (data) => {
          toast({
            title: "Lesson updated",
            description: data.message,
          });
        },
      }
    );
  };

  return (
    <div className='px-2'>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className='flex flex-col gap-2 p-2'>
          <fieldset
            className='flex items-center gap-2 p-2 text-xl'
            disabled={updateLessonMutation.isPending}>
            <Settings2 className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
            Lesson Details
            {form.formState.isDirty && (
              <div className='space-x-1 ms-auto'>
                <Button className='bg-blue-500'>Save</Button>
                <Button
                  variant='outline'
                  type='button'
                  className='border-blue-200'
                  onClick={() => form.reset()}>
                  Reset
                </Button>
              </div>
            )}
          </fieldset>
          <FormField
            control={form.control}
            name='title'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Title</FormLabel>
                <FormControl>
                  <Input className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='description'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Description</FormLabel>
                <FormControl>
                  <Textarea className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name='renewalPrice'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Renewal Price</FormLabel>
                <FormControl>
                  <Input type='number' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='expirationHours'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>
                  Expiration Hours
                </FormLabel>
                <FormControl>
                  <Input type='number' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='videoSrc'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Video Src</FormLabel>
                <FormControl>
                  <Textarea className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </form>
      </Form>
    </div>
  );
}

function LessonVideo({ videoSrc }: { videoSrc: string }) {
  return (
    <div className='flex flex-col gap-4 p-4'>
      <div className='flex items-center justify-between text-xl'>
        <div className='flex items-center gap-2'>
          <ListCollapse className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
          Lesson Content
        </div>
      </div>
      <iframe
        src={videoSrc}
        allowFullScreen
        allow='encrypted-media'
        className='w-full aspect-video'
      />
    </div>
  );
}

export default LessonDetailsPage;
