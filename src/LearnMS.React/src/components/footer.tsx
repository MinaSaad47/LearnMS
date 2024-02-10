import { FaFacebook, FaWhatsapp, FaYoutube } from "react-icons/fa";

const Footer = () => {
  return (
    <footer className='relative'>
      <div className='absolute top-0 left-0 w-full overflow-hidden'>
        <svg
          data-name='Layer 1'
          xmlns='http://www.w3.org/2000/svg'
          viewBox='0 0 1200 120'
          preserveAspectRatio='none'>
          <path
            d='M321.39,56.44c58-10.79,114.16-30.13,172-41.86,82.39-16.72,168.19-17.73,250.45-.39C823.78,31,906.67,72,985.66,92.83c70.05,18.48,146.53,26.09,214.34,3V0H0V27.35A600.21,600.21,0,0,0,321.39,56.44Z'
            className='relative block fill-white'></path>
        </svg>
      </div>
      <div className='w-full text-white bg-color2 h-[250px] flex items-center justify-center gap-5'>
        <a
          href='https://www.facebook.com/people/Newtons-Academy-Mr-Rafik-Isaac/100064151013051'
          className='transition-all duration-300 hover:text-blue-500 hover:cursor-pointer hover:scale-125'>
          <FaFacebook className='w-10 h-10' />
        </a>
        <a
          href='https://www.youtube.com/@newtonacademy9097'
          className='transition-all duration-300 hover:text-red-500 hover:cursor-pointer hover:scale-125'>
          <FaYoutube className='w-10 h-10' />
        </a>
        <a
          href='https://wa.me/1222343492'
          className='transition-all duration-300 hover:text-green-500 hover:cursor-pointer hover:scale-125'>
          <FaWhatsapp className='w-10 h-10' />
        </a>
      </div>
    </footer>
  );
};

export default Footer;
