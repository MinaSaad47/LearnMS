import { toast } from "@/components/ui/use-toast";
import axios, { AxiosError } from "axios";

export class ApiResponse<TData> {
  status: true = true;
  constructor(public message: string, public data: TData) {}
}

export class ApiError<TError = string> extends Error {
  status: false = false;
  constructor(public code: TError, message: string) {
    super(message);
  }
}

export const api = axios.create();

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

api.interceptors.response.use(
  (response) => response,
  (error: AxiosError<ApiError>) => {
    if (error.response?.data?.status === false) {
      const code = import.meta.env.DEV
        ? ` (${error.response?.data?.code})`
        : "";
      toast({
        title: `We couldn't complete your request` + code,
        description: error.response?.data?.message,
        variant: "destructive",
      });
    }
    return Promise.reject(error);
  }
);
