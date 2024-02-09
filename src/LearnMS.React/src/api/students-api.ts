import { ApiResponse, api } from "@/api";
import { validateEgyptianPhoneNumber } from "@/lib/utils";
import { PageList } from "@/types/page-list";
import { Student } from "@/types/students";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const useStudentsQuery = ({
  page,
  pageSize,
  search,
}: {
  page: number;
  pageSize: number;
  search: string | undefined;
}) => {
  return useQuery<ApiResponse<PageList<Student>>>({
    queryKey: ["students", { page, pageSize, search }],
    queryFn: () =>
      api
        .get(`/api/students?page=${page}&pageSize=${pageSize}&search=${search}`)
        .then((res) => res.data),
  });
};

export const CreateStudentRequest = z
  .object({
    email: z.string().email().min(1, { message: "Email is required" }),
    school: z.string().min(1, { message: "School is required" }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
    confirmPassword: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
    fullName: z.string().min(3, { message: "Name is required" }),
    phoneNumber: z.string().refine(validateEgyptianPhoneNumber, {
      message: "Invalid egyptian phone number",
    }),
    parentPhoneNumber: z.string().refine(validateEgyptianPhoneNumber, {
      message: "Invalid egyptian phone number",
    }),
    level: z.enum(["Level0", "Level1", "Level2", "Level3"]),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });

export type CreateStudentRequest = z.infer<typeof CreateStudentRequest>;

export const useCreateStudentMutation = () => {
  const qc = useQueryClient();

  return useMutation<ApiResponse<{}>, {}, CreateStudentRequest>({
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["students"] });
    },
    mutationFn: (data) =>
      api.post("/api/students", data).then((res) => res.data),
  });
};

export const AddStudentCreditRequest = z.object({
  amount: z.coerce
    .number()
    .min(1, { message: "Amount must be greater than 0" }),
});

export type AddStudentCreditRequest = z.infer<typeof AddStudentCreditRequest>;

export const addStudentCreditMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { id: string; data: AddStudentCreditRequest }
  >({
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["students"] });
    },
    mutationFn: ({ id, data }) =>
      api.post(`/api/students/${id}/credit`, data).then((res) => res.data),
  });
};

export const useDeleteStudentMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { id: string }>({
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["students"] });
    },
    mutationFn: ({ id }) =>
      api.delete(`/api/students/${id}`).then((res) => res.data),
  });
};
