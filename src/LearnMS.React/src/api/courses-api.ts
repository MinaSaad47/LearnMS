import { ApiResponse, api } from "@/api";
import { Course, CourseDetails } from "@/types/courses";
import { useQuery } from "@tanstack/react-query";

export const useCourseQuery = (id: string) => {
  return useQuery<ApiResponse<CourseDetails>>({
    queryKey: ["course", id],
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
