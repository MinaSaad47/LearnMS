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
    queryKey: ["lecture", lectureId],
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
      console.log(courseId);
      onSuccess: () => {
        qc.invalidateQueries({ queryKey: ["course", { id: courseId }] });
        qc.invalidateQueries({ queryKey: ["courses"] });
      };
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
