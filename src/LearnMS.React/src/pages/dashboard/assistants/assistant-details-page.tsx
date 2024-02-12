import {
  UpdateAssistantRequest,
  useAssistantQuery,
  useClaimAssistantIncomesMutation,
  useDeleteAssistantMutation,
  useGetAssistantIncomesQuery,
  usePermissionsQuery,
  useUpdateAssistantMutation,
} from "@/api/assistants-api";
import Confirmation from "@/components/confirmation";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardTitle } from "@/components/ui/card";
import { DialogFooter } from "@/components/ui/dialog";
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
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { toast } from "@/lib/utils";
import { assistantIncomesColumns } from "@/pages/dashboard/assistants/columns";
import { AssistantIncomesDataTable } from "@/pages/dashboard/assistants/data-table";
import { Assistant } from "@/types/assistants";
import { zodResolver } from "@hookform/resolvers/zod";
import { PaginationState, RowSelectionState } from "@tanstack/react-table";
import { useState } from "react";
import { useForm } from "react-hook-form";
import { FaMoneyBillAlt, FaMoneyCheckAlt } from "react-icons/fa";
import { useParams } from "react-router-dom";
import { z } from "zod";

const AssistantDetailsPage = () => {
  const { assistantId } = useParams();
  const { data: assistant, isLoading } = useAssistantQuery({
    id: assistantId!,
  });

  if (isLoading) {
    return (
      <div className='flex items-center justify-center w-full h-full'>
        <Loading />
      </div>
    );
  }

  return (
    <div className='w-full h-full p-4'>
      <Tabs
        defaultValue='details'
        className='p-0 border-2 shadow-md rounded-xl shadow-primary border-secondary'>
        <TabsList className='m-0 shadow-sm shadow-primary'>
          <TabsTrigger value='details'>Details</TabsTrigger>
          <TabsTrigger value='incomes'>Incomes</TabsTrigger>
        </TabsList>
        <TabsContent value='details' className='py-6 px-[20%]'>
          <AssistantDetails assistant={assistant!.data} />
        </TabsContent>
        <TabsContent value='incomes' className='p-6'>
          <AssistantIncomes assistant={assistant!.data} />
        </TabsContent>
      </Tabs>
    </div>
  );
};

function AssistantDetails({ assistant }: { assistant: Assistant }) {
  const { data: permissions, isLoading } = usePermissionsQuery();
  const deleteAssistantMutation = useDeleteAssistantMutation();

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

  const onDeleting = () => {
    deleteAssistantMutation.mutate(
      { id: assistant.id },
      {
        onSuccess: () => {
          toast({
            title: "Assistant deleted",
            description: "Assistant deleted successfully",
          });
        },
      }
    );
  };

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
        },
      }
    );
  };

  return (
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
          <DialogFooter className='mt-4'>
            <Confirmation
              button={
                <Button variant='destructive' className='me-auto'>
                  Delete
                </Button>
              }
              title='Are you sure you want to delete this assistant?'
              description='This action cannot be undone.'
              onConfirm={onDeleting}
            />
            <Button type='submit'>Submit</Button>
          </DialogFooter>
        </fieldset>
      </form>
    </Form>
  );
}

function AssistantIncomes({ assistant }: { assistant: Assistant }) {
  const claimAssistantIncomesMutation = useClaimAssistantIncomesMutation();

  const [rowSelection, setRowSelection] = useState<RowSelectionState>({});
  const [{ pageIndex, pageSize }, setPagination] = useState<PaginationState>({
    pageIndex: 0,
    pageSize: 10,
  });

  const { data: incomes, isLoading } = useGetAssistantIncomesQuery({
    id: assistant.id,
    page: pageIndex + 1,
    pageSize,
  });

  if (isLoading) {
    return <Loading />;
  }

  const onClaiming = () => {
    claimAssistantIncomesMutation.mutate(
      { id: assistant.id },
      {
        onSuccess: (res) => {
          toast({
            title: "Incomes claimed",
            description: res.message,
          });
        },
      }
    );
  };

  return (
    <div className='flex flex-col'>
      <div className='grid grid-cols-2 gap-4'>
        <Card className='text-primary'>
          <CardTitle className='flex items-center justify-between p-2 text-4xl'>
            Total Income <FaMoneyCheckAlt />
          </CardTitle>
          <CardContent className='text-3xl'>
            {incomes?.data.totalIncome} LE
          </CardContent>
        </Card>
        <Card className='text-primary'>
          <CardTitle className='flex items-center justify-between p-2 text-4xl'>
            Unclaimed Income <FaMoneyBillAlt />
          </CardTitle>
          <CardContent className='flex justify-between text-3xl'>
            {incomes?.data.unClaimedIncome} LE
            <Button
              disabled={claimAssistantIncomesMutation.isPending}
              onClick={onClaiming}
              variant='outline'
              className='transition-all duration-300 hover:shadow-md hover:shadow-primary hover:text-primary'>
              Claim all
            </Button>
          </CardContent>
        </Card>
      </div>

      <AssistantIncomesDataTable
        rowSelection={rowSelection}
        setRowSelection={setRowSelection}
        pagination={{
          hasNextPage: incomes?.data.data.hasNextPage!,
          hasPreviousPage: incomes?.data.data.hasPreviousPage!,
          pageIndex,
          pageSize,
          pageCount: incomes?.data.data.totalCount!,
        }}
        setPagination={setPagination}
        data={incomes?.data.data.items!}
        columns={assistantIncomesColumns}
      />
    </div>
  );
}

export default AssistantDetailsPage;
