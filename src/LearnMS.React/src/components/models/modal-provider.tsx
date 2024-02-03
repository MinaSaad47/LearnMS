import React from "react";
import { useModalStore } from "../../store/use-modal-store";
import AddExamModal from "./add-exam-modal";
import AddLectureModal from "./add-lecture-modal";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const modals: Record<string, React.FC<any>> = {
  "add-lecture": AddLectureModal,
  "add-exam": AddExamModal,
};

const ModalProvider = () => {
  const { data, type, onClose } = useModalStore();

  const Modal = modals[type];

  if (!Modal) {
    return null;
  }

  // eslint-disable-next-line @typescript-eslint/ban-ts-comment
  // @ts-ignore
  return <Modal {...data} onClose={onClose} />;
};

export default ModalProvider;
