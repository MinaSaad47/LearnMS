import { ApiResponse, api } from "@/api";
import { Assistant, AssistantIncome } from "@/types/assistants";
import { PageList } from "@/types/page-list";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const useAssistantsQuery = () => {
  return useQuery<ApiResponse<{ items: Assistant[] }>>({
    queryKey: ["assistants"],
    queryFn: () => {
      return api.get("/api/administration/assistants").then((res) => res.data);
    },
  });
};

export const usePermissionsQuery = () => {
  return useQuery<ApiResponse<{ items: string[] }>>({
    queryKey: ["permissions"],
    queryFn: () => {
      return api.get("/api/administration/permissions").then((res) => res.data);
    },
  });
};

export const UpdateAssistantRequest = z.object({
  password: z
    .string()
    .optional()
    .transform((val) => (val ? val : undefined)),
  permissions: z.array(z.string().min(1)),
});

export type UpdateAssistantRequest = z.infer<typeof UpdateAssistantRequest>;

export const useUpdateAssistantMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { id: string; data: UpdateAssistantRequest }
  >({
    mutationFn: ({ id, data }) =>
      api
        .patch(`/api/administration/assistants/${id}`, data)
        .then((res) => res.data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["assistants"] });
    },
  });
};

export const CreateAssistantRequest = z.object({
  email: z.string().email().min(1),
  password: z.string().min(1),
});

export type CreateAssistantRequest = z.infer<typeof CreateAssistantRequest>;

export const useCreateAssistantMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, CreateAssistantRequest>({
    mutationFn: (data) =>
      api.post("/api/administration/assistants", data).then((res) => res.data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["assistants"] });
    },
  });
};

export const useDeleteAssistantMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { id: string }>({
    mutationFn: ({ id }) =>
      api
        .delete(`/api/administration/assistants/${id}`)
        .then((res) => res.data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["assistants"] });
    },
  });
};

export const useAssistantQuery = ({ id }: { id: string }) => {
  return useQuery<ApiResponse<Assistant>>({
    queryKey: ["assistant", { id }],
    queryFn: () => {
      return api
        .get(`/api/administration/assistants/${id}`)
        .then((res) => res.data);
    },
  });
};

export const useGetAssistantIncomesQuery = ({
  id,
  page,
  pageSize,
}: {
  id: string;
  page: number;
  pageSize: number;
}) => {
  return useQuery<
    ApiResponse<{
      unClaimedIncome: number;
      totalIncome: number;
      data: PageList<AssistantIncome>;
    }>
  >({
    queryKey: ["assistant-incomes", { id, page, pageSize }],
    queryFn: () => {
      return api
        .get(
          `/api/administration/assistants/${id}/incomes?page=${page}&pageSize=${pageSize}`
        )
        .then((res) => res.data);
    },
  });
};

export const useClaimAssistantIncomesMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { id: string }>({
    mutationFn: ({ id }) =>
      api
        .post(`/api/administration/assistants/${id}/claim`)
        .then((res) => res.data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["assistant-incomes"] });
    },
  });
};
