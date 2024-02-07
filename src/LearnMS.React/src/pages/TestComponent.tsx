import React from 'react'

const TestComponent = () => {
  return (
    <div className='root'>
      <header className={classes.header}>
        {/* Header content goes here */}
      </header>
      <section className={classes.homeAbout}>
        <div className={classes.content}>
          <Typography variant="h1" gutterBottom>
            Mr Rafik Isaac
          </Typography>
          <Typography variant="body1" gutterBottom>
            Don't waste your time, check out our productive courses. Our self-improvement courses are very effective.
          </Typography>
          <Button variant="contained" color="primary" className={classes.button} href="course.html">
            Let's start
          </Button>
        </div>
        <div className={classes.boxContainer}>
          <div className={classes.box}>
            <i className="fas fa-graduation-cap"></i>
            <Typography variant="h3" gutterBottom>Mission</Typography>
            <Typography variant="body1">
              We foster our students’ love for learning, encourage them to try new and exciting things, and give them a solid foundation to build on.
            </Typography>
          </div>
          <div className={classes.box}>
            <i className="fas fa-fire"></i>
            <Typography variant="h3" gutterBottom>Vision</Typography>
            <Typography variant="body1">
              Our vision is to develop well-rounded, confident, and responsible individuals who aspire to achieve their full potential.
            </Typography>
          </div>
          <div className={classes.box}>
            <i className="fas fa-award"></i>
            <Typography variant="h3" gutterBottom>Goal</Typography>
            <Typography variant="body1">
              If you are looking for high-quality and reliable online courses, it will be us.
            </Typography>
          </div>
        </div>
      </section>
      <section className={classes.about} id="about-about">
        <Typography variant="h1" className={classes.headingAbout}>about us</Typography>
        <div className={classes.aboutContent}>
          <div className={classes.aboutText}>
            <Typography variant="h3">start your journey to a better life with our courses</Typography>
            <Typography variant="body1">
              Welcome to the Coursing - all available online courses!
              All in one platform <br /> Lectures - Exams - daily quizzes
            </Typography>
            <Button variant="contained" color="primary" href="payment.html">Buy credits</Button>
          </div>
          <div className={classes.image}>
            <img src="https://img.freepik.com/free-vector/web-development-programmer-engineering-coding-website-augmented-reality-interface-screens-developer-project-engineer-programming-software-application-design-cartoon-illustration_107791-3863.jpg?size=626&ext=jpg" alt="Web Development" />
          </div>
        </div>
      </section>
      <section className={classes.footer}>
        <div className={classes.iconContainer}>
          <a href="https://www.facebook.com" className="fab fa-facebook-f" target="_new" className={classes.icon}></a>
          <a href="https://twitter.com/" className="fab fa-twitter" target="_new" className={classes.icon}></a>
          <a href="https://www.instagram.com/" className="fab fa-instagram" target="_new" className={classes.icon}></a>
        </div>
      </section>
    </div>
  );
};


export default TestComponent