import { ApiResponse, api } from "@/api";
import { Assistant } from "@/types/assistants";
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
