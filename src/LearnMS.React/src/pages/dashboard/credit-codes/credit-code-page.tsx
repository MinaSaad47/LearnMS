import {
  GenerateCreditCodeRequest,
  useGenerateCreditCodeMutation,
  useGetCreditCodesQuery,
  useSellCreditCodesMutation,
} from "@/api/credits-api";
import Loading from "@/components/loading/loading";
import { Button } from "@/components/ui/button";
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
import { columns } from "@/pages/dashboard/credit-codes/columns";
import { CreditCodesDataTable } from "@/pages/dashboard/credit-codes/data-table";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  PaginationState,
  RowSelectionState,
  SortingState,
} from "@tanstack/react-table";
import { FileWarningIcon, Loader2 } from "lucide-react";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import { useSearchParams } from "react-router-dom";

const CreditCodesPage = () => {
  const form = useForm({
    resolver: zodResolver(GenerateCreditCodeRequest),
    defaultValues: {
      count: 0,
      value: 0,
    },
  });

  const generateCreditCodes = useGenerateCreditCodeMutation();
  const sellCreditCodesMutation = useSellCreditCodesMutation();

  const onSell = (codes: string[]) => {
    sellCreditCodesMutation.mutate(
      { codes },
      {
        onSuccess(data) {
          toast({
            title: "Credit codes sold",
            description: data.message,
          });
        },
      }
    );
  };

  const onSubmit = (data: GenerateCreditCodeRequest) => {
    generateCreditCodes.mutate(data, {
      onSuccess: () => {
        form.reset();
      },
    });
  };

  const [searchParams, setSearchParams] = useSearchParams({});
  const [{ pageIndex, pageSize }, setPagination] = useState<PaginationState>({
    pageIndex: Number(searchParams.get("page") ?? 1) - 1,
    pageSize: Number(searchParams.get("pageSize") ?? 10),
  });
  const [rowSelection, setRowSelection] = useState<RowSelectionState>({});
  const [search, setSearch] = useState(searchParams.get("search") ?? "");
  const [sorting, setStorting] = useState<SortingState>([]);

  useEffect(() => {
    setSearchParams({ page: `${pageIndex + 1}`, pageSize: `${pageSize}` });
  }, [pageIndex, pageSize]);

  var sortOrder: any;
  if (sorting.filter((s) => s.id === "status")[0]?.desc === true) {
    sortOrder = "desc";
  } else if (sorting.filter((s) => s.id === "status")[0]?.desc === false) {
    sortOrder = "asc";
  }

  const query = useGetCreditCodesQuery({
    page: pageIndex + 1,
    pageSize: pageSize,
    search,
    sortOrder,
  });

  if (query.isError) {
    <div className='flex flex-col items-center justify-center w-full h-full'>
      <FileWarningIcon className='w-4 h-4 text-pink-800' />
      <p>Error loading credit codes</p>
      <div>{query.error.message}</div>
    </div>;
  }

  const selectedCodes =
    Object.keys(rowSelection).length > 0 ? Object.keys(rowSelection) : null;

  console.log(sorting);

  return (
    <div className='w-full h-full'>
      <Form {...form}>
        <form
          onSubmit={form.handleSubmit(onSubmit)}
          className='flex flex-col items-center gap-2 py-4'>
          <fieldset
            disabled={generateCreditCodes.isPending}
            className='grid grid-cols-2 gap-2 w-[80%] m-auto'>
            <FormField
              control={form.control}
              name='count'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Count</FormLabel>
                  <FormControl>
                    <Input type='number' className='text-blue-500' {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />

            <FormField
              control={form.control}
              name='value'
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Value</FormLabel>
                  <FormControl>
                    <Input type='number' className='text-blue-500' {...field} />
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
          </fieldset>
          <Button
            disabled={generateCreditCodes.isPending}
            className='transition-all duration-200 hover:scale-105'>
            {generateCreditCodes.isPending ? (
              <div className='flex items-center gap-2'>
                <Loader2 className='animate-spin' />
                Generating...
              </div>
            ) : (
              <>Generate</>
            )}
          </Button>
        </form>
      </Form>
      <div className='flex items-center justify-center gap-2 mx-6'>
        {selectedCodes && (
          <Button
            type='button'
            onClick={() => onSell(selectedCodes)}
            disabled={sellCreditCodesMutation.isPending}
            className='flex-grow text-3xl text-blue-600 transition-all duration-300 bg-blue-300 border-2 border-blue-600 hover:text-white hover:scale-105 h-14'>
            Sell
          </Button>
        )}
        <div className='w-[80%] ms-auto px-2 py-2 bg-blue-300 border border-blue-500 rounded-xl'>
          <input
            placeholder='Filter Codes...'
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            className='w-full p-2 text-blue-500 rounded-xl focus:outline-none focus:border-none'
          />
        </div>
      </div>
      {query.isLoading ? (
        <Loading />
      ) : (
        <CreditCodesDataTable
          sorting={sorting}
          setSorting={setStorting}
          pagination={{
            hasNextPage: query.data!.data.hasNextPage,
            hasPreviousPage: query.data!.data.hasPreviousPage,
            pageIndex,
            pageSize,
            pageCount: query.data!.data.totalCount,
          }}
          columns={columns}
          rowSelection={rowSelection}
          setRowSelection={setRowSelection}
          data={query.data!.data.items}
          setPagination={setPagination}
        />
      )}
    </div>
  );
};

export default CreditCodesPage;
