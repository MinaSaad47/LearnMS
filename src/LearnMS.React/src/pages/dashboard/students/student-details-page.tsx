import {
  UpdateStudentRequest,
  useDeleteStudentMutation,
  useStudentQuery,
  useUpdateStudentMutation,
} from "@/api/students-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Separator } from "@/components/ui/separator";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { toast } from "@/components/ui/use-toast";
import { zodResolver } from "@hookform/resolvers/zod";
import { ResetIcon } from "@radix-ui/react-icons";
import { Save, Trash } from "lucide-react";
import { useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import { getFirstCharacters } from "../../../lib/utils";

const StudentDetailsPage = () => {
  const { studentId } = useParams();

  return (
    <div className='w-full h-full p-0 '>
      <Tabs defaultValue='profile' className='w-full h-full m-0'>
        <TabsList>
          <TabsTrigger value='profile'>Profile</TabsTrigger>
        </TabsList>
        <TabsContent value='profile'>
          <StudentProfile studentId={studentId!} />
        </TabsContent>
      </Tabs>
    </div>
  );
};

function StudentProfile({ studentId }: { studentId: string }) {
  const { data: student, isLoading } = useStudentQuery({ id: studentId! });
  const updateStudentMutation = useUpdateStudentMutation();
  const deleteStudentMutation = useDeleteStudentMutation();
  const navigate = useNavigate();

  const form = useForm<UpdateStudentRequest>({
    resolver: zodResolver(UpdateStudentRequest),
    defaultValues: {
      fullName: student?.data.fullName ?? "",
      phoneNumber: student?.data.phoneNumber ?? "",
      parentPhoneNumber: student?.data.parentPhoneNumber ?? "",
      schoolName: student?.data.schoolName ?? "",
      level: student?.data.level ?? "Level0",
    },
    values: {
      fullName: student?.data.fullName ?? "",
      phoneNumber: student?.data.phoneNumber ?? "",
      parentPhoneNumber: student?.data.parentPhoneNumber ?? "",
      schoolName: student?.data.schoolName ?? "",
      level: student?.data.level ?? "Level0",
    },
  });

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  const onSubmit = (data: UpdateStudentRequest) => {
    updateStudentMutation.mutate(
      { id: student!.data.id, data },
      {
        onSuccess: () => {
          toast({
            title: "Profile updated",
            description: "Your profile has been updated",
          });
        },
      }
    );
  };

  const onDelete = () => {
    deleteStudentMutation.mutate(
      { id: student!.data.id },
      {
        onSuccess: () => {
          toast({
            title: "Student deleted",
            description: "Student deleted successfully",
          });
          navigate("/dashboard/students");
        },
      }
    );
  };

  return (
    <div className='p-4 space-y-4'>
      <div className='flex items-center gap-2 p-4 border-2 rounded shadow-md text-primary border-secondary shadow-primary'>
        <Avatar className='w-24 h-24 shadow-xl shadow-primary'>
          <AvatarImage src={student!.data.profilePicture} />
          <AvatarFallback className='text-3xl shadow-md bg-primary/30 text-primary-foreground'>
            {getFirstCharacters(student!.data.fullName)}
          </AvatarFallback>
        </Avatar>
        <div>
          <h1>{student!.data.email}</h1>
          <p>{student!.data.id}</p>
        </div>
        <Badge className='ms-auto'>{student!.data.credit} LE</Badge>
      </div>
      <Separator />
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className='p-4 border-2 rounded shadow-md border-secondary shadow-primary'>
          <fieldset disabled={updateStudentMutation.isPending}>
            <FormField
              control={form.control}
              name='fullName'
              render={({ field }) => (
                <FormItem>
                  <FormItem>
                    <FormLabel>Full Name</FormLabel>
                    <FormDescription>Your full name</FormDescription>
                    <FormControl>
                      <Input {...field} placeholder='John Doe' />
                    </FormControl>
                  </FormItem>
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='phoneNumber'
              render={({ field }) => (
                <FormItem>
                  <FormItem>
                    <FormLabel>Phone Number</FormLabel>
                    <FormDescription>Student's phone number</FormDescription>
                    <FormControl>
                      <Input {...field} placeholder='John Doe' />
                    </FormControl>
                  </FormItem>
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='parentPhoneNumber'
              render={({ field }) => (
                <FormItem>
                  <FormItem>
                    <FormLabel>Parent Phone Number</FormLabel>
                    <FormDescription>Parent's phone number</FormDescription>
                    <FormControl>
                      <Input {...field} placeholder='John Doe' />
                    </FormControl>
                  </FormItem>
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name='level'
              render={({ field }) => (
                <FormItem>
                  <FormItem>
                    <FormLabel>Level</FormLabel>
                    <FormDescription>Student's level</FormDescription>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}>
                      <FormControl>
                        <SelectTrigger>
                          <SelectValue placeholder='Select your level' />
                        </SelectTrigger>
                      </FormControl>
                      <SelectContent>
                        <SelectItem value='Level0'>3rd Prep School</SelectItem>
                        <SelectItem value='Level1'>
                          1st Secondary School
                        </SelectItem>
                        <SelectItem value='Level2'>
                          2st Secondary School
                        </SelectItem>
                        <SelectItem value='Level3'>
                          3st Secondary School
                        </SelectItem>
                      </SelectContent>
                    </Select>
                  </FormItem>
                </FormItem>
              )}
            />
            <div className='flex items-center justify-between mt-4'>
              <Confirmation
                button={
                  <Button
                    type='button'
                    variant='destructive'
                    disabled={deleteStudentMutation.isPending}>
                    <Trash /> Delete
                  </Button>
                }
                description='Are you sure you want to delete this student?'
                onConfirm={onDelete}
                title='Delete Student'
                disabled={deleteStudentMutation.isPending}
              />

              {form.formState.isDirty && (
                <div className='flex items-center gap-2'>
                  <Button
                    type='button'
                    onClick={() => form.reset()}
                    className='border-primary'
                    variant='outline'>
                    <ResetIcon /> Reset
                  </Button>
                  <Button type='submit'>
                    <Save /> Save
                  </Button>
                </div>
              )}
            </div>
          </fieldset>
        </form>
      </Form>
    </div>
  );
}

export default StudentDetailsPage;
