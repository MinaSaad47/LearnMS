import { ApiResponse, api } from "@/api";
import { CreditCode } from "@/types/credit-code";
import { PageList } from "@/types/page-list";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const RedeemRequest = z.object({
  code: z.string().min(1, { message: "Code is required" }),
});

export type RedeemRequest = z.infer<typeof RedeemRequest>;

export type RedeemResponse = {
  value: number;
};

export const useRedeemMutation = () => {
  const qc = useQueryClient();

  return useMutation<ApiResponse<RedeemResponse>, {}, RedeemRequest>({
    mutationKey: ["redeem"],
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["profile"] });
    },
    mutationFn: (data) =>
      api
        .put(`/api/credit-codes/redeem?code=${data.code}`)
        .then((res) => res.data),
  });
};

export const useGetCreditCodesQuery = ({
  page,
  pageSize,
  search,
}: {
  page: number;
  pageSize: number;
  search: string | undefined;
}) => {
  const query = useQuery<ApiResponse<PageList<CreditCode>>>({
    queryKey: ["credit-codes", { page, pageSize, search }],
    queryFn: () =>
      api
        .get(
          `/api/credit-codes?page=${page}&pageSize=${pageSize}&search=${search}`
        )
        .then((res) => res.data),
  });

  return query;
};

export const GenerateCreditCodeRequest = z.object({
  count: z.coerce
    .number()
    .min(1, { message: "Count must be greater than 0" })
    .max(100, { message: "Count must be less than 100" }),
  value: z.coerce
    .number()
    .positive()
    .max(1000, { message: "Value must be less than 1000" }),
});

export type GenerateCreditCodeRequest = z.infer<
  typeof GenerateCreditCodeRequest
>;

export const useGenerateCreditCodeMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, GenerateCreditCodeRequest>({
    mutationKey: ["generate-credit-code"],
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["credit-codes"] });
    },
    mutationFn: (data) =>
      api.post(`/api/credit-codes`, data).then((res) => res.data),
  });
};
