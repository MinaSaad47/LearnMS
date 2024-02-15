import { ApiResponse, api } from "@/api";
import { LectureDetails } from "@/types/lectures";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { z } from "zod";

export const useLectureQuery = ({
  lectureId,
  courseId,
}: {
  lectureId: string;
  courseId: string;
}) => {
  return useQuery<ApiResponse<LectureDetails>>({
    queryKey: ["lecture", { id: lectureId, courseId }],
    queryFn: () =>
      api
        .get(`/api/courses/${courseId}/lectures/${lectureId}`)
        .then((res) => res.data),
  });
};

export const AddLectureRequest = z.object({
  title: z.string().min(1, { message: "Title is required" }),
});

export type AddLectureRequest = z.infer<typeof AddLectureRequest>;

export const useAddLectureMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { courseId: string; data: AddLectureRequest }
  >({
    mutationFn: ({ courseId, data }) =>
      api
        .post(`/api/courses/${courseId}/lectures`, data)
        .then((res) => res.data),
    onSuccess: (_, { courseId }) => {
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
  });
};

export const UpdateLectureRequest = z.object({
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
});

export type UpdateLectureRequest = z.infer<typeof UpdateLectureRequest>;

export const useUpdateLectureMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { courseId: string; lectureId: string; data: UpdateLectureRequest }
  >({
    onSuccess: (_, { lectureId }) => {
      qc.invalidateQueries({ queryKey: ["lecture", { id: lectureId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ lectureId, courseId, data }) =>
      api
        .patch(`/api/courses/${courseId}/lectures/${lectureId}`, data)
        .then((res) => res.data),
  });
};

export const usePublishingLectureMutation = () => {
  const qc = useQueryClient();

  return useMutation<
    ApiResponse<{}>,
    {},
    { courseId: string; lectureId: string; publish: boolean }
  >({
    mutationFn: ({ courseId, lectureId, publish }) =>
      api
        .post(
          `/api/courses/${courseId}/lectures/${lectureId}/${
            publish ? "publish" : "unpublish"
          }`
        )
        .then((res) => res.data),
    onSuccess: (_, { lectureId, courseId }) => {
      qc.invalidateQueries({
        queryKey: ["lecture", { id: lectureId, courseId }],
      });
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
  });
};

export const useDeleteLectureMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { lectureId: string; courseId: string }
  >({
    onSuccess: (_, { lectureId, courseId }) => {
      qc.invalidateQueries({
        queryKey: ["lecture", { id: lectureId, courseId }],
      });
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
    },
    mutationFn: ({ lectureId, courseId }) =>
      api
        .delete(`/api/courses/${courseId}/lectures/${lectureId}`)
        .then((res) => res.data),
  });
};

export const useBuyLectureMutation = () => {
  const qc = useQueryClient();
  return useMutation<
    ApiResponse<{}>,
    {},
    { lectureId: string; courseId: string }
  >({
    onSuccess: (_, { lectureId, courseId }) => {
      qc.invalidateQueries({
        queryKey: ["lecture", { id: lectureId, courseId }],
      });
      qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
      qc.invalidateQueries({ queryKey: ["courses"] });
      qc.invalidateQueries({ queryKey: ["profile"] });
    },
    mutationFn: ({ lectureId, courseId }) =>
      api
        .post(`/api/courses/${courseId}/lectures/${lectureId}/buy`)
        .then((res) => res.data),
  });
};
