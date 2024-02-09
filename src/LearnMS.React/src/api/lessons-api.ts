import { ApiResponse, api } from "@/api";
import { LessonDetails } from "@/types/lessons";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

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
    queryKey: ["lesson", { id: lessonId }],
    queryFn: () => {
      return api
        .get(
          `/api/courses/${courseId}/lectures/${lectureId}/lessons/${lessonId}`
        )
        .then((res) => res.data);
    },
  });
};

export const UpdateLessonRequest = z.object({
  title: z.string().min(1, { message: "Title is required" }),
  videoSrc: z.string().min(1, { message: "Video is required" }),
  description: z.string(),
});

export type UpdateLessonRequest = z.infer<typeof UpdateLessonRequest>;

export const useUpdateLessonMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    {
      courseId: string;
      lectureId: string;
      lessonId: string;
      data: UpdateLessonRequest;
    }
  >({
    onSuccess: (_, { courseId, lectureId, lessonId }) => {
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({ queryKey: ["lecture", { id: lectureId }] });
      qc.invalidateQueries({ queryKey: ["lesson", { id: lessonId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ courseId, lectureId, lessonId, data }) =>
      api
        .patch(
          `/api/courses/${courseId}/lectures/${lectureId}/lessons/${lessonId}`,
          data
        )
        .then((res) => res.data),
  });
};

export const AddLessonRequest = z.object({
  title: z.string().min(1, { message: "Title is required" }),
  VideoSrc: z.string().min(1, { message: "Video is required" }),
  description: z.string().min(1, { message: "Description is required" }),
});

export type AddLessonRequest = z.infer<typeof AddLessonRequest>;

export const useAddLessonMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    {
      courseId: string;
      lectureId: string;
      data: AddLessonRequest;
    }
  >({
    onSuccess: (_, { courseId, lectureId }) => {
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({
        queryKey: ["lecture", { id: lectureId, courseId }],
      });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ courseId, lectureId, data }) => {
      const formData = new FormData();
      formData.append("title", data.title);
      formData.append("VideoSrc", data.VideoSrc);
      formData.append("description", data.description);
      return api
        .post(
          `/api/courses/${courseId}/lectures/${lectureId}/lessons`,
          formData
        )
        .then((res) => res.data);
    },
  });
};

export const useDeleteLessonMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { courseId: string; lectureId: string; lessonId: string }
  >({
    onSuccess: (_, { courseId, lectureId }) => {
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({
        queryKey: ["lecture", { id: lectureId, courseId }],
      });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ courseId, lectureId, lessonId }) =>
      api
        .delete(
          `/api/courses/${courseId}/lectures/${lectureId}/lessons/${lessonId}`
        )
        .then((res) => res.data),
  });
};
