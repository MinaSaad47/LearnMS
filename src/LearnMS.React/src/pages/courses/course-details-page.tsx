import CoverPicker from "@/components/cover-picker";
import EditableInput from "@/components/editable-input";
import EditableTextarea from "@/components/editable-textarea";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Form, FormField, FormItem, FormMessage } from "@/components/ui/form";
import { useModalStore } from "@/store/use-modal-store";
import { CourseDetails } from "@/types/courses";
import {
  MoreHorizontal,
  PencilRuler,
  PlusCircle,
  VideotapeIcon,
} from "lucide-react";
import { useForm } from "react-hook-form";
import { Link, useParams } from "react-router-dom";

const course: CourseDetails = {
  id: "1",
  name: "Course Name",
  description: "Course Description",
  coverUrl: "https://via.placeholder.com/150",
  price: 100,
  renewPrice: 200,
  items: [
    {
      id: "1",
      order: 1,
      type: "lecture",
      name: "Lecture Name",
      price: 10,
      renewPrice: 20,
    },
    {
      type: "exam",
      id: "2",
      order: 2,
      name: "Exam Name",
      price: 10,
      renewPrice: 20,
    },
  ],
};

const CourseDetailsPage = () => {
  const { openModal } = useModalStore();
  const { id } = useParams();

  console.log(id);

  return (
    <div className='flex flex-col items-center w-full gap-2 p-2'>
      <CourseDetailsHeader {...course} />
      <div className='flex gap-2'>
        <Button onClick={() => openModal("add-exam")}>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Exam
        </Button>
        <Button onClick={() => openModal("add-lecture")}>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Lecture
        </Button>
      </div>
      {<CourseItemsList items={course.items} courseId={course.id} />}
    </div>
  );
};

export default CourseDetailsPage;

interface CourseItemsProps {
  courseId: string;
  items: CourseDetails["items"];
}

function CourseItemsList({ courseId, items }: CourseItemsProps) {
  return items.map((item) => (
    <CourseItem key={item.order} courseId={courseId} item={item} />
  ));
}

function CourseDetailsHeader({
  name,
  description,
  coverUrl,
  price,
  renewPrice,
}: typeof course) {
  const form = useForm({
    defaultValues: {
      name,
      description,
      price,
      renewPrice,
      coverUrl: "",
    },
    values: {
      name,
      description,
      price,
      renewPrice,
      coverUrl: "",
    },
  });

  const isDirty = form.formState.isDirty;

  return (
    <Form {...form}>
      <form
        className='flex items-start gap-2'
        onSubmit={form.handleSubmit(() => {})}>
        <div className='w-[20%]'>
          <CoverPicker
            defaultCover={coverUrl}
            onCoverSelect={(url) => form.setValue("coverUrl", url)}
          />
        </div>
        <div className='flex flex-col items-start flex-grow gap-4 mt-2 ms-4'>
          <FormField
            control={form.control}
            name='name'
            render={({ field }) => (
              <FormItem>
                <EditableInput {...field} type='text' />
              </FormItem>
            )}
          />
          <div className='flex gap-2'>
            <FormField
              control={form.control}
              name='price'
              render={({ field }) => (
                <FormItem>
                  <EditableInput {...field} type='number' />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='renewPrice'
              render={({ field }) => (
                <FormItem>
                  <EditableInput {...field} type='number' />
                  <FormMessage />
                </FormItem>
              )}
            />
          </div>
          <FormField
            control={form.control}
            name='description'
            render={({ field }) => (
              <FormItem className='w-full'>
                <EditableTextarea placeholder='ex Mathematics' {...field} />
                <FormMessage />
              </FormItem>
            )}
          />
          {isDirty && (
            <div className='flex self-end gap-2'>
              <Button
                type='button'
                variant='secondary'
                onClick={() => form.reset()}>
                Reset
              </Button>
              <Button type='submit'>Save</Button>
            </div>
          )}
        </div>
      </form>
    </Form>
  );
}

function CourseItem({
  courseId,
  item,
}: {
  courseId: string;
  item: (typeof course)["items"][0];
}) {
  return (
    <div className='flex w-full gap-10 p-2 border rounded'>
      <div className='flex items-center justify-center text-sm border rounded-full w-7 h-7 dark:border-white'>
        {item.order}
      </div>
      {item.type === "lecture" && (
        <Badge>
          <VideotapeIcon />
          Lecture
        </Badge>
      )}
      {item.type === "exam" && (
        <Badge variant='secondary'>
          <PencilRuler />
          Exam
        </Badge>
      )}
      <p>{item.price}</p>
      <p>{item.renewPrice}</p>
      <div className='ml-auto'>
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant='ghost' className='w-8 h-8 p-0'>
              <span className='sr-only'>Open menu</span>
              <MoreHorizontal className='w-4 h-4' />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align='end'>
            <DropdownMenuLabel>Actions</DropdownMenuLabel>
            <DropdownMenuItem
              onClick={() => navigator.clipboard.writeText(item.id)}>
              Copy Item ID
            </DropdownMenuItem>
            <DropdownMenuSeparator />
            <Link to={`/courses/${courseId}/${item.type}s/${item.order}`}>
              <DropdownMenuItem>View Item</DropdownMenuItem>
            </Link>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </div>
  );
}
