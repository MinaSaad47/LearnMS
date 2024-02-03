import { create } from "zustand";

type State = {
  type: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  data: any;
  isOpen: boolean;
};

type Actions = {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  openModal: (type: string, data?: any) => void;
  onClose: () => void;
};

export const useModalStore = create<State & Actions>((set) => ({
  type: "",
  data: {},
  isOpen: false,
  openModal: (type, data = {}) => set({ type, data, isOpen: true }),
  onClose: () => set({ type: "", data: {}, isOpen: false }),
}));
