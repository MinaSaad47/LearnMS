import { useLectureQuery } from "@/api/lectures-api";
import Loading from "@/components/loading/loading";
import { useParams } from "react-router-dom";

const StudentLecturePage = () => {
  const { lectureId, courseId } = useParams();

  const { isLoading, data: _ } = useLectureQuery({
    lectureId: lectureId!,
    courseId: courseId!,
  });

  if (isLoading) {
    return <Loading />;
  }

  return <div>StudentLecturePage</div>;
};

export default StudentLecturePage;
