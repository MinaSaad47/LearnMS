import {
  CreateAssistantRequest,
  useCreateAssistantMutation,
} from "@/api/assistants-api";
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
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm } from "react-hook-form";

interface AddAssistantModalProps {
  onClose: () => void;
}

const AddAssistantModal: React.FC<AddAssistantModalProps> = ({ onClose }) => {
  const createAssistantMutation = useCreateAssistantMutation();

  const form = useForm({
    resolver: zodResolver(CreateAssistantRequest),
    values: {
      email: "",
      password: "",
    },
  });

  const onSubmit = (data: CreateAssistantRequest) => {
    createAssistantMutation.mutate(data, {
      onSuccess: () => {
        toast({
          title: "Assistant created",
          description: "Assistant created successfully",
        });
        onClose();
      },
    });
  };

  return (
    <Dialog open onOpenChange={onClose}>
      <DialogContent className='sm:max-w-[425px]'>
        <DialogHeader>
          <DialogTitle>Create Assistant</DialogTitle>
          <DialogDescription>Create a new Assistant</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset
              disabled={createAssistantMutation.isPending}
              className='flex flex-col gap-2'>
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

export default AddAssistantModal;
