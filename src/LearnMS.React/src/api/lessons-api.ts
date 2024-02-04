import { ApiResponse, api } from "@/api";
import { LessonDetails } from "@/types/lessons";
import { useQuery } from "@tanstack/react-query";

export const useLessonsQuery = ({
  courseId,
  lectureId,
  lessonId,
}: {
  courseId: string;
  lectureId: string;
  lessonId: string;
}) => {
  return useQuery<ApiResponse<LessonDetails>>({
    queryKey: ["lesson", lessonId],
    queryFn: () => {
      return api
        .get(
          `/api/courses/${courseId}/lectures/${lectureId}/lessons/${lessonId}`
        )
        .then((res) => res.data);
    },
  });
};
