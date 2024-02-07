import AddAssistantModal from "@/pages/dashboard/assistants/add-assistant-modal";
import UpdateAssistantModal from "@/pages/dashboard/assistants/update-assistant-modal";
import AddStudentModal from "@/pages/dashboard/students/add-student-modal";
import React from "react";
import { useModalStore } from "../../store/use-modal-store";

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const modals: Record<string, React.FC<any>> = {
  "add-assistant-modal": AddAssistantModal,
  "add-student-modal": AddStudentModal,
  "update-assistant-modal": UpdateAssistantModal,
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
