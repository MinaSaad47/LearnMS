export type Exam = {
  type: "exam";
  id: string;
  order: number;
  name: string;
  price: number;
  renewPrice?: number;
};
