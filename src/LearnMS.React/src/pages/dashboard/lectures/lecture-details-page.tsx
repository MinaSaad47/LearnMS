import {
  UpdateLectureRequest,
  useDeleteLectureMutation,
  useLectureQuery,
  usePublishingLectureMutation,
  useUpdateLectureMutation,
} from "@/api/lectures-api";
import { AddLessonRequest, useAddLessonMutation } from "@/api/lessons-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
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
import { LectureDetails } from "@/types/lectures";
import { Lesson } from "@/types/lessons";
import { Quiz } from "@/types/quiz";
import { zodResolver } from "@hookform/resolvers/zod";
import { Edit2, ListCollapse, LucideMove, Menu, Settings2 } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate, useParams } from "react-router-dom";

const LectureDetailsPage = () => {
  const { courseId, lectureId } = useParams();
  const navigate = useNavigate();

  console.log({ courseId, lectureId });

  const {
    data: lecture,
    isLoading,
    isError,
  } = useLectureQuery({ courseId: courseId!, lectureId: lectureId! });

  const publishingLectureMutation = usePublishingLectureMutation();
  const deleteLectureMutation = useDeleteLectureMutation();

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  const onPublish = () => {
    publishingLectureMutation.mutate(
      {
        lectureId: lectureId!,
        publish: !lecture!.data.isPublished!,
        courseId: courseId!,
      },
      {
        onSuccess() {
          toast({
            title: "Publishing",
            description: lecture?.data.isPublished
              ? "Successfully unpublished the course"
              : "Successfully published the course",
          });
        },
      }
    );
  };

  const onDeleting = () => {
    deleteLectureMutation.mutate(
      {
        lectureId: lectureId!,
        courseId: courseId!,
      },
      {
        onSuccess() {
          navigate(`/dashboard/courses/${courseId}`, {
            replace: true,
          });
          toast({
            title: "Deleting",
            description: "Successfully deleted the lecture",
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
        <h1 className='text-3xl'>Lecture Setup</h1>
        <div className='flex gap-2 item-center'>
          <Confirmation
            button={<Button variant='destructive'>Delete</Button>}
            title='Are you sure you want to delete this lecture?'
            description='This action cannot be undone.'
            onConfirm={onDeleting}
          />

          <Button
            disabled={publishingLectureMutation.isPending}
            onClick={onPublish}
            className='text-blue-500 bg-white border border-blue-500 rounded hover:bg-blue-500 hover:text-white'>
            {lecture?.data.isPublished ? "UnPublish" : "Publish"}
          </Button>
        </div>
      </div>

      <div className='grid w-full grid-cols-2 mt-10'>
        <LectureDetailsForm {...lecture?.data!} courseId={courseId!} />
        <LectureContentForm {...lecture?.data!} courseId={courseId!} />
      </div>
    </div>
  );
};

function LectureDetailsForm({
  id,
  description,
  title,
  expirationDays,
  imageUrl,
  renewalPrice,
  courseId,
  price,
}: LectureDetails & { courseId: string }) {
  const updateLectureMutation = useUpdateLectureMutation();

  const form = useForm<UpdateLectureRequest>({
    resolver: zodResolver(UpdateLectureRequest),
    defaultValues: {
      imageUrl,
      description,
      title,
      expirationDays,
      renewalPrice,
      price,
    },
    values: {
      description,
      imageUrl,
      title,
      expirationDays,
      renewalPrice,
      price,
    },
  });

  const onSubmit = (data: UpdateLectureRequest) => {
    updateLectureMutation.mutate(
      { lectureId: id, courseId, data },
      {
        onSuccess: (data) => {
          toast({
            title: "Lecture updated",
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
            disabled={updateLectureMutation.isPending}>
            <Settings2 className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
            Lecture Details
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
            name='price'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Price</FormLabel>
                <FormControl>
                  <Input type='number' className='text-blue-500' {...field} />
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
                <FormLabel className='text-blue-500'>RenewalPrice</FormLabel>
                <FormControl>
                  <Input type='number' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='expirationDays'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Expiration Days</FormLabel>
                <FormControl>
                  <Input type='number' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='imageUrl'
            render={({ field }) => (
              <FormItem className='p-3 bg-blue-200 border-2 border-blue-400 rounded'>
                <FormLabel className='text-blue-500'>Image Url</FormLabel>
                <FormControl>
                  <Input className='text-blue-500' {...field} />
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

function LectureContentForm({
  items,
  id: lectureId,
  courseId,
}: LectureDetails & { courseId: string }) {
  const [isAddingLesson, setIsAddingLecture] = useState(false);
  const [isAddingQuiz, setIsAddingExam] = useState(false);
  return (
    <div className='flex flex-col gap-4 p-4'>
      <div className='flex items-center justify-between text-xl'>
        <div className='flex items-center gap-2'>
          <ListCollapse className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
          Lecture Content
        </div>
        <div className='flex items-center justify-center gap-2'>
          {!isAddingLesson && !isAddingQuiz ? (
            <>
              <DropdownMenu>
                <DropdownMenuTrigger>
                  <Menu />
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                  <DropdownMenuItem
                    className='hover:bg-blue-400 hover:text-white hover:cursor-pointer'
                    onClick={() => setIsAddingLecture(true)}>
                    Add Lesson
                  </DropdownMenuItem>
                  <DropdownMenuSeparator />
                  <DropdownMenuItem
                    className='hover:bg-blue-400 hover:text-white hover:cursor-pointer'
                    onClick={() => setIsAddingExam(true)}>
                    Add Quiz
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            </>
          ) : (
            <Button
              variant='destructive'
              onClick={() => {
                setIsAddingLecture(false);
                setIsAddingExam(false);
              }}>
              Cancel
            </Button>
          )}
        </div>
      </div>
      {isAddingLesson && !isAddingQuiz && (
        <AddLessonForm
          courseId={courseId}
          lectureId={lectureId}
          onClose={() => setIsAddingLecture(false)}
        />
      )}
      {isAddingQuiz && !isAddingLesson && (
        <AddQuizForm
          courseId={lectureId}
          onClose={() => setIsAddingExam(false)}
        />
      )}
      {!isAddingQuiz && !isAddingLesson && (
        <div className='flex flex-col gap-2'>
          {items.map((item) => (
            <LectureItem
              key={item.order}
              item={item}
              courseId={courseId}
              lectureId={lectureId}
            />
          ))}
        </div>
      )}
    </div>
  );
}

function LectureItem({
  item,
  courseId,
  lectureId,
}: {
  item: Quiz | Lesson;
  courseId: string;
  lectureId: string;
}) {
  return (
    <div className='flex items-center justify-between w-full gap-2 text-blue-500 bg-blue-100 border border-blue-300 rounded'>
      <div className='flex gap-2'>
        <div className='flex items-center justify-center w-10 h-full p-2 text-blue-400 border-blue-300 border-e hover:cursor-grab'>
          <LucideMove />
        </div>
        <div className='p-2'>{item.title}</div>
      </div>
      <div className='flex items-center gap-2'>
        <Badge className='h-5'>{item.type}</Badge>
        <Link
          className='me-2'
          to={`/dashboard/courses/${courseId}/lectures/${lectureId}/${item.type.toLowerCase()}s/${
            item.id
          }`}>
          <Edit2 className='w-4 h-4' />
        </Link>
      </div>
    </div>
  );
}

function AddLessonForm({
  courseId,
  lectureId,
  onClose,
}: {
  courseId: string;
  lectureId: string;
  onClose: () => void;
}) {
  const addLecture = useAddLessonMutation();

  const form = useForm<AddLessonRequest>({
    resolver: zodResolver(AddLessonRequest),
    defaultValues: {
      title: "",
      description: "",
      VideoSrc: "",
      expirationHours: 0,
      renewalPrice: 0,
    },
  });

  const onSubmit = (data: AddLessonRequest) => {
    console.log(data);
    addLecture.mutate(
      { courseId, lectureId, data },
      {
        onSuccess: (data) => {
          console.log(data);
          toast({
            title: "Lesson added",
            description: data.message,
          });
          onClose();
        },
      }
    );
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)}>
        <fieldset
          className='p-2 space-y-2 border-2 border-blue-400 rounded'
          disabled={addLecture.isPending}>
          <FormField
            control={form.control}
            name='title'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='text-blue-500'>Title</FormLabel>
                <FormControl>
                  <Input type='text' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='description'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='text-blue-500'>Description</FormLabel>
                <FormControl>
                  <Input type='text' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name='renewalPrice'
            render={({ field }) => (
              <FormItem>
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
              <FormItem>
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
            name='VideoSrc'
            render={({ field }) => (
              <FormItem>
                <FormLabel className='text-blue-500'>Video Src</FormLabel>
                <FormControl>
                  <Input type='text' className='text-blue-500' {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button type='submit'>Add</Button>
        </fieldset>
      </form>
    </Form>
  );
}

function AddQuizForm({ courseId }: { courseId: string; onClose: () => void }) {
  return <div></div>;
}

export default LectureDetailsPage;
