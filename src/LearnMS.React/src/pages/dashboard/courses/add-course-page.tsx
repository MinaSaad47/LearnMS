import { AddCourseRequest, useAddCourseMutation } from "@/api/courses-api";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { toast } from "@/components/ui/use-toast";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";

const AddCoursePage = () => {
  const addCourseMutation = useAddCourseMutation();
  const navigate = useNavigate();

  const form = useForm<AddCourseRequest>({
    resolver: zodResolver(AddCourseRequest),
    values: {
      title: "",
    },
  });

  const onSubmit = (data: AddCourseRequest) => {
    addCourseMutation.mutate(data, {
      onSuccess: (res) => {
        toast({
          title: "Course created",
          description: res.message,
        });
        navigate(`/dashboard/courses/${res.data.id}`, { replace: true });
      },
    });
  };

  return (
    <div className='flex items-center justify-center w-full h-full'>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className='border rounded w-[50%] p-10 space-y-2'>
          <FormField
            control={form.control}
            name='title'
            render={({ field }) => (
              <FormItem className='w-full'>
                <FormLabel>Title</FormLabel>
                <FormControl>
                  <Input placeholder='ex Mathematics' {...field} />
                </FormControl>
                <FormDescription>The title of the course.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <div className='space-x-2'>
            <Link to='/dashboard/courses'>
              <Button className='m-auto' type='button' variant='outline'>
                Cancel
              </Button>
            </Link>
            <Button
              className='m-auto'
              type='submit'
              disabled={addCourseMutation.isPending}>
              Submit
            </Button>
          </div>
        </form>
      </Form>
    </div>
  );
};

export default AddCoursePage;
