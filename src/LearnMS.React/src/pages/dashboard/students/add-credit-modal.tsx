import {
  AddStudentCreditRequest,
  addStudentCreditMutation as useAddStudentCreditMutation,
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
import { toast } from "@/components/ui/use-toast";
import { Student } from "@/types/students";
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm } from "react-hook-form";

interface AddCreditModalProps {
  onClose: () => void;
  student: Student;
}

const AddCreditModal: React.FC<AddCreditModalProps> = ({
  onClose,
  student,
}) => {
  const addStudentCredit = useAddStudentCreditMutation();

  const form = useForm<AddStudentCreditRequest>({
    resolver: zodResolver(AddStudentCreditRequest),
    values: {
      amount: 0,
    },
    defaultValues: {
      amount: 0,
    },
  });

  const onSubmit = (data: AddStudentCreditRequest) => {
    addStudentCredit.mutate(
      { id: student.id, data },
      {
        onSuccess: () => {
          toast({
            title: "Added Credit",
            description: "Added credit to student successfully",
          });
          onClose();
        },
      }
    );
  };

  return (
    <Dialog open onOpenChange={onClose}>
      <DialogContent className='sm:max-w-[425px]'>
        <DialogHeader>
          <DialogTitle>Adding Credit</DialogTitle>
          <DialogDescription>
            Adding Credit to {student.fullName}
          </DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset
              disabled={addStudentCredit.isPending}
              className='flex flex-col gap-2'>
              <FormField
                name='amount'
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Amount</FormLabel>
                    <FormControl>
                      <Input type='number' {...field} />
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

export default AddCreditModal;
