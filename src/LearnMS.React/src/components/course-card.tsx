import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Link } from "react-router-dom";

const course = {
  name: "Course Name",
  description: "Course Description",
  coverUrl: "https://via.placeholder.com/150",
  price: 100,
  renewPrice: 200,
};

type CourseCardProps = { course: typeof course };

const CourseCard: React.FC<CourseCardProps> = ({ course }) => {
  const { name, description, coverUrl, price, renewPrice } = course;

  return (
    <Link to='/courses/some-course-id'>
      <Card className='transition hover:scale-105 hover:cursor-pointer'>
        <CardHeader>
          <CardTitle>{name}</CardTitle>
          <CardDescription>{description}</CardDescription>
        </CardHeader>
        <CardContent>
          <img
            src={coverUrl}
            alt=''
            className='object-cover w-full h-full rounded-lg'
          />
        </CardContent>
        <CardFooter>
          Price
          {renewPrice ? (
            <>
              <p className='ml-auto mr-2 line-through'>${price}</p>
              <p>${renewPrice}</p>
            </>
          ) : (
            <>
              <p className='ml-auto text-end'>${price}</p>
            </>
          )}
        </CardFooter>
      </Card>
    </Link>
  );
};

export default CourseCard;
