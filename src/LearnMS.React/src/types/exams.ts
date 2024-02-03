export type Exam = {
  type: "Exam";
  id: string;
  order: number;
  title: string;
  price: number;
  renewalPrice?: number;
  isExpired?: boolean;
  expiresAt?: Date;
};
