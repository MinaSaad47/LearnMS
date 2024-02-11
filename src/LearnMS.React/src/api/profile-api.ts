import { ApiResponse, api } from "@/api";
import { validateEgyptianPhoneNumber } from "@/lib/utils";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export type Permission =
  | "ManageCourses"
  | "ManageStudents"
  | "ManageAssistants"
  | "ManageFiles";

export type Profile = {
  id: string;
  email: string;
} & (
  | {
      role: "Student";
      fullName: string;
      level: "Level0" | "Level1" | "Level2" | "Level3";
      phoneNumber: string;
      parentPhoneNumber: string;
      school: string;
      profilePicture: string;
      credits: number;
    }
  | {
      role: "Teacher";
    }
  | {
      role: "Assistant";
      permissions: Permission[];
    }
);

export type ProfileState =
  | ({
      isAuthenticated: true;
    } & Profile)
  | {
      isAuthenticated: false;
    };

export const useProfileQuery = () => {
  const query = useQuery<ProfileState>({
    queryKey: ["profile"],
    retry: false,
    queryFn: () => {
      if (!localStorage.getItem("token")) return { isAuthenticated: false };
      return api.get<ApiResponse<Profile>>("/api/profile").then((res) => {
        if (res.data.status) {
          return { isAuthenticated: true, ...res.data.data };
        } else {
          return { isAuthenticated: false };
        }
      });
    },
  });

  return { profile: query.data, ...query };
};

export const UpdateProfileRequest = z.object({
  fullName: z.string().min(1, { message: "Name is required" }).nullish(),
  phoneNumber: z
    .string()
    .refine(validateEgyptianPhoneNumber, {
      message: "Invalid egyptian phone number",
    })
    .nullish(),
  parentPhoneNumber: z
    .string()
    .refine(validateEgyptianPhoneNumber, {
      message: "Invalid egyptian phone number",
    })
    .nullish(),
  password: z
    .string()
    .min(8, { message: "Password must be at least 8 characters" })
    .nullish(),
  confirmPassword: z
    .string()
    .min(8, { message: "Password must be at least 8 characters" })
    .nullish(),
  level: z.enum(["Level0", "Level1", "Level2", "Level3"]).nullish(),
  school: z.string().min(1, { message: "School is required" }).nullish(),
});

export type UpdateProfileRequest = z.infer<typeof UpdateProfileRequest>;

export const useUpdateProfileMutation = () => {
  const qc = useQueryClient();

  return useMutation<ApiResponse<{}>, {}, UpdateProfileRequest>({
    mutationFn: (data) =>
      api.patch("/api/profile", data).then((res) => res.data),
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["profile"] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
  });
};
