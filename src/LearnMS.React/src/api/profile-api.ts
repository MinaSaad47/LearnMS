import { ApiResponse, api } from "@/api";
import { toast } from "@/components/ui/use-toast";
import { useQuery } from "@tanstack/react-query";
import { useEffect } from "react";

type Permission = "view" | "create" | "update" | "delete";

export type Profile = {
  id: string;
  name: string;
  email: string;
} & (
  | {
      role: "student";
    }
  | {
      role: "teacher";
    }
  | {
      role: "assistant";
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

  useEffect(() => {
    if (query.isSuccess && query.data.isAuthenticated) {
      toast({
        title: "Greeting",
        description: `Welcome ${query.data.email}`,
        variant: "default",
      });
    }
  }, [query.isSuccess]);

  return { profile: query.data, ...query };
};
