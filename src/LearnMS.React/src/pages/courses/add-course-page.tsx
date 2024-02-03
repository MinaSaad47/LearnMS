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
import { Textarea } from "@/components/ui/textarea";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";
import CoverPicker from "../../components/cover-picker";
import { useToast } from "../../components/ui/use-toast";

const formSchema = z.object({
  name: z.string(),
  description: z.string(),
  price: z.coerce.number().positive(),
  renewPrice: z.coerce.number().positive(),
  coverUrl: z.string().url(),
});

const AddCoursePage = () => {
  const { toast } = useToast();

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    values: {
      name: "",
      description: "",
      price: 100,
      renewPrice: 20,
      coverUrl: "",
    },
  });

  const onSubmit = (data: z.infer<typeof formSchema>) => {
    console.log(data);
    toast({
      title: "Course created",
      description: "Your course has been created.",
    });
  };

  return (
    <div className='mx-auto mt-20 w-fit'>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className='flex flex-col items-start gap-2'>
          <div className='w-full h-56'>
            <CoverPicker
              onCoverSelect={(url) => form.setValue("coverUrl", url)}
            />
          </div>
          <FormField
            control={form.control}
            name='name'
            render={({ field }) => (
              <FormItem className='w-full'>
                <FormLabel>Name</FormLabel>
                <FormControl>
                  <Input placeholder='ex Mathematics' {...field} />
                </FormControl>
                <FormDescription>The name of the course.</FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <div className='flex gap-2'>
            <FormField
              control={form.control}
              name='price'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Price</FormLabel>
                  <FormControl>
                    <Input type='number' placeholder='99' {...field} />
                  </FormControl>
                  <FormDescription>The price of the course.</FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='renewPrice'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Renew Price</FormLabel>
                  <FormControl>
                    <Input type='number' placeholder='20' {...field} />
                  </FormControl>
                  <FormDescription>
                    The renew price of the course.
                  </FormDescription>
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
                <FormLabel>Description</FormLabel>
                <FormControl>
                  <Textarea
                    placeholder='Mathematics will be covered'
                    {...field}
                  />
                </FormControl>
                <FormDescription>
                  The Description of the course.
                </FormDescription>
                <FormMessage />
              </FormItem>
            )}
          />
          <Button className='self-end' type='submit'>
            Submit
          </Button>
        </form>
      </Form>
    </div>
  );
};

export default AddCoursePage;
