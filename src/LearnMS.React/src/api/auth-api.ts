import { ApiResponse, api } from "@/api";
import { toast } from "@/components/ui/use-toast";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const LoginRequest = z.object({
  email: z.string().email().min(1, { message: "Email is required" }),
  password: z.string().min(1, { message: "Password is required" }),
});

export type LoginRequest = z.infer<typeof LoginRequest>;
export type LoginResponse = {
  id: string;
  token: string;
};

export const RegisterRequest = z
  .object({
    level: z.enum(["0", "1", "2"], {
      errorMap: () => ({ message: "Level is required" }),
    }),
    fullName: z.string().min(1, { message: "Name is required" }),
    phoneNumber: z.string().min(1, { message: "Phone number is required" }),
    parentPhoneNumber: z
      .string()
      .min(1, { message: "Parent phone number is required" }),
    school: z.string().min(1, { message: "School is required" }),
    email: z.string().email().min(1, { message: "Email is required" }),
    password: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
    confirmPassword: z
      .string()
      .min(8, { message: "Password must be at least 8 characters" }),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords do not match",
    path: ["confirmPassword"],
  });
export type RegisterRequest = z.infer<typeof RegisterRequest>;
export type RegisterResponse = {
  id: string;
};

export function useLoginMutation() {
  const qrc = useQueryClient();
  return useMutation<ApiResponse<LoginResponse>, {}, LoginRequest>({
    mutationKey: ["login"],
    mutationFn: (data) =>
      api.post("/api/auth/login", data).then((res) => res.data),
    onSuccess: (res) => {
      if (!res.status) return;
      toast({
        title: "Login successful",
        description: "Welcome back!",
      });
      localStorage.setItem("token", res.data.token);
      qrc.invalidateQueries({ queryKey: ["account"] });
    },
  });
}

export function useRegisterMutation() {
  const qrc = useQueryClient();
  return useMutation<ApiResponse<RegisterResponse>, {}, RegisterRequest>({
    mutationKey: ["register"],
    mutationFn: (data) =>
      api.post("/api/auth/students/register", data).then((res) => res.data),
    onSuccess: (res) => {
      toast({
        title: "Register successful",
        description: `registered successfully with id ${res.data.id}`,
      });
      qrc.invalidateQueries({ queryKey: ["account"] });
    },
  });
}

export function useLogoutMutation() {
  const qrc = useQueryClient();
  return useMutation({
    mutationKey: ["logout"],
    mutationFn: async () => {},
    onSuccess: () => {
      localStorage.removeItem("token");
      qrc.invalidateQueries({ queryKey: ["profile"] });
    },
  });
}
