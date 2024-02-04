import { ApiResponse, api } from "@/api";
import { Course, CourseDetails } from "@/types/courses";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

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
    onSuccess: () => {
      qc.invalidateQueries({ queryKey: ["profile"] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ courseId }) =>
      api.post(`/api/courses/${courseId}/buy`).then((res) => res.data),
  });
};
