import { ApiResponse, api } from "@/api";
import { Course, CourseDetails } from "@/types/courses";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const AddCourseRequest = z.object({
  title: z.string().min(1, { message: "Title is required" }),
});

export type AddCourseRequest = z.infer<typeof AddCourseRequest>;

type AddCourseResponse = {
  id: string;
};

export const useAddCourseMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<AddCourseResponse>, {}, AddCourseRequest>({
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: (data) =>
      api.post("/api/courses", data).then((res) => res.data),
  });
};

export const UpdateCourseRequest = z.object({
  title: z.string().min(1, { message: "Title is required" }),
  description: z.string(),
  price: z.coerce.number().min(0, { message: "Price must be greater than 0" }),
  renewalPrice: z.coerce
    .number()
    .min(0, { message: "Renewal Price is greater than 0" }),
  expirationDays: z.coerce
    .number()
    .min(0, { message: "Expiration days must be greater than 0" }),
  imageUrl: z.string().url().min(1, { message: "Image URL is required" }),
  level: z.enum(["Level0", "Level1", "Level2", "Level3"]),
});

export type UpdateCourseRequest = z.infer<typeof UpdateCourseRequest>;

export const useUpdateCourseMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { id: string; data: UpdateCourseRequest }
  >({
    onSuccess: (_, { id }) => {
      qc.invalidateQueries({ queryKey: ["course", { id }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ id, data }) =>
      api.patch(`/api/courses/${id}`, data).then((res) => res.data),
  });
};

export const useCourseQuery = (id: string) => {
  return useQuery<ApiResponse<CourseDetails>>({
    queryKey: ["course", { id }],
    queryFn: () => api.get(`/api/courses/${id}`).then((res) => res.data),
  });
};

export const useCoursesQuery = () => {
  const query = useQuery<ApiResponse<{ items: Course[] }>>({
    queryKey: ["courses"],
    queryFn: () => {
      return api.get("/api/courses").then((res) => res.data);
    },
  });

  return query;
};

export const useBuyCourseMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { courseId: string }>({
    onSuccess: (_, { courseId }) => {
      qc.invalidateQueries({ queryKey: ["profile"] });
      qc.invalidateQueries({ queryKey: ["courses"] });
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
    },
    mutationFn: ({ courseId }) =>
      api.post(`/api/courses/${courseId}/buy`).then((res) => res.data),
  });
};

export const usePublishingCourseMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { id: string; publish: boolean }>({
    onSuccess: (_, { id }) => {
      qc.invalidateQueries({ queryKey: ["course", { id }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ id, publish }) =>
      api
        .post(`/api/courses/${id}/${publish ? "publish" : "unpublish"}`)
        .then((res) => res.data),
  });
};

export const useDeleteCourseMutation = () => {
  const qc = useQueryClient();
  return useMutation<ApiResponse<{}>, {}, { id: string }>({
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ id }) =>
      api.delete(`/api/courses/${id}`).then((res) => res.data),
  });
};
