import { useGetCreditCodesQuery } from "@/api/credit-codes-api";
import { Card, CardTitle } from "@/components/ui/card";
import { FileWarningIcon, Loader2 } from "lucide-react";
import { Fragment } from "react";

const CreditCodesPage = () => {
  const query = useGetCreditCodesQuery();

  if (query.isLoading) {
    return (
      <div className='flex flex-col items-center justify-center w-full h-full'>
        <Loader2 className='w-4 h-4 animate-spin' />
        <p>Loading credit codes</p>
      </div>
    );
  }

  if (query.isError) {
    <div className='flex flex-col items-center justify-center w-full h-full'>
      <FileWarningIcon className='w-4 h-4 text-pink-800' />
      <p>Error loading credit codes</p>
      <div>{query.error.message}</div>
    </div>;
  }

  console.log(query.data);

  return (
    query.data &&
    query.data?.data.items.map((creditCode) => (
      <Fragment key={creditCode.code}>
        <Card>
          <CardTitle>{creditCode.code}</CardTitle>
          <p>{creditCode.value}</p>
        </Card>
      </Fragment>
    ))
  );
};

export default CreditCodesPage;
