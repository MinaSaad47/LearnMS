import {
  useCourseQuery,
  usePublishingCourseMutation,
  useUpdateCourseMutation,
} from "@/api/courses-api";
import { AddLectureRequest, useAddLectureMutation } from "@/api/lectures-api";
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
import { CourseDetails } from "@/types/courses";
import { Exam } from "@/types/exams";
import { Lecture } from "@/types/lectures";
import { zodResolver } from "@hookform/resolvers/zod";
import { Edit2, ListCollapse, LucideMove, Menu, Settings2 } from "lucide-react";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useParams } from "react-router-dom";
import { UpdateCourseRequest } from "../../../api/courses-api";

const CourseDetailsPage = () => {
  const { courseId } = useParams();

  const { data: course, isLoading, isError } = useCourseQuery(courseId!);

  const publishingCourseMutation = usePublishingCourseMutation();

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  if (isError) {
    return;
  }

  const onPublishing = () => {
    publishingCourseMutation.mutate(
      { id: course!.data.id!, publish: !course!.data.isPublished! },
      {
        onSuccess() {
          toast({
            title: "Publishing",
            description: course?.data.isPublished
              ? "Successfully unpublished the course"
              : "Successfully published the course",
          });
        },
      }
    );
  };

  return (
    <div className='w-full h-full p-4'>
      <div className='flex justify-between w-full'>
        <h1 className='text-3xl'>Course Setup</h1>
        <div className='flex gap-2 item-center'>
          <Button
            onClick={onPublishing}
            className='text-blue-500 bg-white border border-blue-500 rounded hover:bg-blue-500 hover:text-white'>
            {course?.data.isPublished ? "UnPublish" : "Publish"}
          </Button>
        </div>
      </div>

      <div className='grid w-full grid-cols-2 mt-10'>
        <CourseDetailsForm {...course?.data!} />
        <CourseContentForm {...course?.data!} />
      </div>
    </div>
  );
};

function CourseDetailsForm({
  id,
  description,
  title,
  expirationDays,
  renewalPrice,
  imageUrl,
  price,
}: CourseDetails) {
  const updateCourseMutation = useUpdateCourseMutation();

  const form = useForm<UpdateCourseRequest>({
    resolver: zodResolver(UpdateCourseRequest),
    defaultValues: {
      description,
      title,
      expirationDays,
      renewalPrice,
      price,
      imageUrl,
    },
    values: {
      description,
      title,
      expirationDays,
      renewalPrice,
      price,
      imageUrl,
    },
  });

  const onSubmit = (data: UpdateCourseRequest) => {
    updateCourseMutation.mutate(
      { id, data },
      {
        onSuccess: (data) => {
          toast({
            title: "Course updated",
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
            disabled={updateCourseMutation.isPending}>
            <Settings2 className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
            Course Details
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

function CourseContentForm({ items, id }: CourseDetails) {
  const [isAddingLecture, setIsAddingLecture] = useState(false);
  const [isAddingExam, setIsAddingExam] = useState(false);
  return (
    <div className='flex flex-col gap-4 p-4'>
      <div className='flex items-center justify-between text-xl'>
        <div className='flex items-center gap-2'>
          <ListCollapse className='text-blue-400 bg-blue-200 rounded-[50%] w-10 h-10 p-1' />
          Course Content
        </div>
        <div className='flex items-center justify-center gap-2'>
          {!isAddingLecture && !isAddingExam ? (
            <>
              <DropdownMenu>
                <DropdownMenuTrigger>
                  <Menu />
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                  <DropdownMenuItem
                    className='hover:bg-blue-400 hover:text-white hover:cursor-pointer'
                    onClick={() => setIsAddingLecture(true)}>
                    Add Lecture
                  </DropdownMenuItem>
                  <DropdownMenuSeparator />
                  <DropdownMenuItem
                    className='hover:bg-blue-400 hover:text-white hover:cursor-pointer'
                    onClick={() => setIsAddingExam(true)}>
                    Add Exam
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
      {isAddingLecture && !isAddingExam && (
        <AddLectureForm
          courseId={id}
          onClose={() => setIsAddingLecture(false)}
        />
      )}
      {isAddingExam && !isAddingLecture && (
        <AddExamForm courseId={id} onClose={() => setIsAddingExam(false)} />
      )}
      {!isAddingExam && !isAddingLecture && (
        <div className='flex flex-col gap-2'>
          {items.map((item) => (
            <CourseItem key={item.order} item={item} courseId={id} />
          ))}
        </div>
      )}
    </div>
  );
}

function CourseItem({
  item,
  courseId,
}: {
  item: Exam | Lecture;
  courseId: string;
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
          to={`/dashboard/courses/${courseId}/${item.type.toLowerCase()}s/${
            item.id
          }`}>
          <Edit2 className='w-4 h-4' />
        </Link>
      </div>
    </div>
  );
}

function AddLectureForm({
  courseId,
  onClose,
}: {
  courseId: string;
  onClose: () => void;
}) {
  const addLecture = useAddLectureMutation();

  const form = useForm({
    resolver: zodResolver(AddLectureRequest),
    defaultValues: {
      title: "",
    },
  });

  const onSubmit = (data: AddLectureRequest) => {
    addLecture.mutate(
      { courseId, data },
      {
        onSuccess: (data) => {
          toast({
            title: "Lecture added",
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
          <Button type='submit'>Add</Button>
        </fieldset>
      </form>
    </Form>
  );
}

function AddExamForm({ courseId }: { courseId: string; onClose: () => void }) {
  return <div></div>;
}

export default CourseDetailsPage;
