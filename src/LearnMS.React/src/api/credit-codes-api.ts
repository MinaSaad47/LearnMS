import { ApiResponse, api } from "@/api";
import { CreditCode } from "@/types/credit-code";
import { useQuery } from "@tanstack/react-query";

const getCreditCodes = async () => {
  await new Promise((resolve) => setTimeout(resolve, 1000));
  return api
    .get<ApiResponse<{ items: CreditCode[] }>>("/api/credit-codes")
    .then((res) => res.data);
};

export const useGetCreditCodesQuery = () => {
  const query = useQuery({
    queryKey: ["credit-codes"],
    queryFn: getCreditCodes,
  });

  return query;
};
