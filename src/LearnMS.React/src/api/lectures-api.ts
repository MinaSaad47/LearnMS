import { ApiResponse, api } from "@/api";
import { LectureDetails } from "@/types/lectures";
import { useQuery } from "@tanstack/react-query";

export const useLectureQuery = ({
  lectureId,
  courseId,
}: {
  lectureId: string;
  courseId: string;
}) => {
  return useQuery<ApiResponse<LectureDetails>>({
    queryKey: ["lecture", lectureId],
    queryFn: () =>
      api
        .get(`/api/courses/${courseId}/lectures/${lectureId}`)
        .then((res) => res.data),
  });
};
