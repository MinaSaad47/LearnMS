import { ApiResponse, api } from "@/api";
import { useMutation, useQueryClient } from "@tanstack/react-query";
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
