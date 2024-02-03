import EditableInput from "@/components/editable-input";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import
  {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu";
import { Form, FormField, FormItem, FormMessage } from "@/components/ui/form";
import { useModalStore } from "@/store/use-modal-store";
import
  {
    MoreHorizontal,
    PencilRuler,
    PlusCircle,
    VideotapeIcon,
  } from "lucide-react";
import { useForm } from "react-hook-form";
import { Link, useParams } from "react-router-dom";

const lecture = {
  id: "1",
  name: "Lecture Name",
  price: 100,
  renewPrice: 200,
  items: [
    {
      id: "1",
      order: 1,
      type: "lesson",
      price: 100,
      renewPrice: 20,
    },
    { id: "1", order: 2, type: "quiz", price: 20 },
  ],
};

const LectureDetailsPage = () => {
  const { openModal } = useModalStore();
  const { courseId, lectureId } = useParams();

  return (
    <div className='flex flex-col items-center w-full gap-2 p-2'>
      <LectureDetailsHeader {...lecture} />
      <div className='flex gap-2'>
        <Button onClick={() => openModal("add-exam")}>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Quiz
        </Button>
        <Button onClick={() => openModal("add-lecture")}>
          <PlusCircle className='w-4 h-4 mr-2' />
          Add Lesson
        </Button>
      </div>
      {lecture.items.map((item) => (
        <LectureItem
          key={item.order}
          courseId={courseId!}
          lectureId={lectureId!}
          item={item}
        />
      ))}
    </div>
  );
};

export default LectureDetailsPage;

function LectureDetailsHeader({ name, price, renewPrice }: typeof lecture) {
  const form = useForm({
    defaultValues: {
      name,
      price,
      renewPrice,
    },
    values: {
      name,
      price,
      renewPrice,
    },
  });

  const isDirty = form.formState.isDirty;

  return (
    <Form {...form}>
      <form
        className='flex items-start gap-2'
        onSubmit={form.handleSubmit(() => {})}>
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

interface LectureItemProps {
  lectureId: string;
  item: (typeof lecture)["items"][0];
  courseId: string;
}

function LectureItem({ courseId, lectureId, item }: LectureItemProps) {
  return (
    <div className='flex w-full gap-10 p-2 border rounded'>
      <div className='flex items-center justify-center text-sm border rounded-full w-7 h-7 dark:border-white'>
        {item.order}
      </div>
      {item.type === "lecture" && (
        <Badge>
          <VideotapeIcon />
          Lesson
        </Badge>
      )}
      {item.type === "exam" && (
        <Badge variant='secondary'>
          <PencilRuler />
          Quiz
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
            <Link
              to={`/courses/${courseId}/lectures/${lectureId}/${item.type}s/${item.order}`}>
              <DropdownMenuItem>View Item</DropdownMenuItem>
            </Link>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </div>
  );
}
