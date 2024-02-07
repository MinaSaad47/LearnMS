import {
  UpdateAssistantRequest,
  usePermissionsQuery,
  useUpdateAssistantMutation,
} from "@/api/assistants-api";
import Loading from "@/components/loading/loading";
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
import { Switch } from "@/components/ui/switch";
import { toast } from "@/components/ui/use-toast";
import { Assistant } from "@/types/assistants";
import { zodResolver } from "@hookform/resolvers/zod";
import React from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";

interface UpdateAssistantModal {
  onClose: () => void;
  assistant: Assistant;
}

const UpdateAssistantModal: React.FC<UpdateAssistantModal> = ({
  onClose,
  assistant,
}) => {
  const { data: permissions, isLoading } = usePermissionsQuery();

  const updateAssistantMutation = useUpdateAssistantMutation();

  const PermissionsSchema = permissions?.data.items.reduce(
    (acc, value) => ({ ...acc, [value]: z.boolean() }),
    {}
  );

  const PasswordPermissionsSchema = z.object({
    password: z
      .string()
      .optional()
      .transform((val) => (val ? val : undefined)),
    ...PermissionsSchema,
  });

  const permissionsValues = permissions?.data.items.reduce(
    (acc, value) => ({
      ...acc,
      [value]: assistant.permissions.includes(value),
    }),
    {}
  );

  const form = useForm<z.infer<typeof PasswordPermissionsSchema>>({
    resolver: zodResolver(PasswordPermissionsSchema),
    values: {
      password: "",
      ...permissionsValues,
    },
  });

  if (isLoading) {
    return <Loading />;
  }

  const onSubmit = (data: z.infer<typeof PasswordPermissionsSchema>) => {
    const { password, ...perms } = data;
    const request = UpdateAssistantRequest.parse({
      password,
      permissions: perms
        ? permissions?.data.items.filter((p) => (perms as any)[p])
        : [],
    });
    updateAssistantMutation.mutate(
      { id: assistant.id, data: request },
      {
        onSuccess: () => {
          toast({
            title: "Assistant updated",
            description: "Assistant updated successfully",
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
          <DialogTitle>Update Assistant</DialogTitle>
          <DialogDescription>Updating {assistant.email}</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)}>
            <fieldset
              disabled={updateAssistantMutation.isPending}
              className='flex flex-col gap-2'>
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
              {permissions?.data.items.map((p) => (
                <FormField
                  key={p}
                  control={form.control}
                  name={p as any}
                  render={({ field }) => (
                    <FormItem className='flex flex-row items-center justify-between p-4 border rounded-lg'>
                      <div className='space-y-0.5'>
                        <FormLabel className='text-base'>{p}</FormLabel>
                      </div>
                      <FormControl>
                        <Switch
                          checked={field.value}
                          onCheckedChange={field.onChange}
                          aria-readonly
                        />
                      </FormControl>
                    </FormItem>
                  )}
                />
              ))}
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

export default UpdateAssistantModal;
