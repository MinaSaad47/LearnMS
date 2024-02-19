import {
  CreateStudentRequest,
  useCreateStudentMutation,
} from "@/api/students-api";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { toast } from "@/components/ui/use-toast";
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm } from "react-hook-form";

interface AddStudentModalProps {
  onClose: () => void;
}

const AddStudentModal: React.FC<AddStudentModalProps> = ({ onClose }) => {
  const createStudentMutation = useCreateStudentMutation();

  const form = useForm<CreateStudentRequest>({
    resolver: zodResolver(CreateStudentRequest),
    values: {
      email: "",
      password: "",
      confirmPassword: "",
      fullName: "",
      level: "Level0",
      school: "",
      parentPhoneNumber: "",
      studentCode: "",
      phoneNumber: "",
    },
    defaultValues: {
      email: "",
      password: "",
      confirmPassword: "",
      fullName: "",
      level: "Level0",
      school: "",
      parentPhoneNumber: "",
      studentCode: "",
      phoneNumber: "",
    },
  });

  const onSubmit = (data: CreateStudentRequest) => {
    createStudentMutation.mutate(data, {
      onSuccess: () => {
        toast({
          title: "Student created",
          description: "Student created successfully",
        });
        onClose();
      },
    });
  };

  return (
    <Dialog open onOpenChange={onClose}>
      <DialogContent className='sm:max-w-[425px]'>
        <DialogHeader>
          <DialogTitle>Create Student</DialogTitle>
          <DialogDescription>Create a new Student</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset
              disabled={createStudentMutation.isPending}
              className='flex flex-col gap-2'>
              <FormField
                name='fullName'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Full Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='email'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Email</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='phoneNumber'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Phone Number</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='parentPhoneNumber'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel> Parent Phone Number</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='studentCode'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel> Student Code </FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='school'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>School Name</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='level'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Level</FormLabel>
                    <Select
                      onValueChange={field.onChange}
                      defaultValue={field.value}>
                      <FormControl>
                        <SelectTrigger>
                          <SelectValue placeholder='Select a level' />
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
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='password'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Password</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormField
                name='confirmPassword'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Confirm Password</FormLabel>
                    <FormControl>
                      <Input {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <DialogFooter>
                <Button type='submit'>Submit</Button>
              </DialogFooter>
            </fieldset>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  );
};

export default AddStudentModal;
